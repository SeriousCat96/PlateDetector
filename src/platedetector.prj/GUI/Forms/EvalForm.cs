using OpenCvSharp.UserInterface;

using PlateDetector.Controllers;
using PlateDetector.Detection;
using PlateDetector.Evaluation;
using PlateDetector.Logging;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlateDetector.GUI.Forms
{
    public partial class EvalForm : Form
	{
        #region Data

        private EvaluationController _evaluationController;

        private Progress<ProgressReport> _progress;

        private CancellationTokenSource _cts;

        #endregion
        
        #region .ctor

        public EvalForm(Detector detector, PictureBoxIpl picBox, Log log, string initFolder)
        {
            InitializeComponent();

            progressBar.Visible = false;
            progressBar.Style = ProgressBarStyle.Continuous;
            
            Detector = detector;
            Log = log;
            PicBox = picBox;

            _evaluationController = new EvaluationController(
                new List<IMetric>
                {
                    new RecallMetric(),
                    new PrecisionMetric()
                },
                Detector,
                PicBox,
                Log)
            {
                Folder = initFolder
            };

            tboxFolder.Text = _evaluationController.Folder;

            listViewMetrics
                .Items
                .AddRange(_evaluationController.Metrics.Select(e => new ListViewItem(e.ToString())).ToArray());

            _progress = new Progress<ProgressReport>();
            _progress.ProgressChanged += (s, args) =>
            {
                progressBar.Style   = ProgressBarStyle.Continuous;
                progressBar.Maximum = args.ItemsCount;
                progressBar.Value   = args.CurPosition;
                lblFileName.Text    = args.File;
            };
        }


        #endregion

        #region Properties

        public Detector Detector { get; }

        public Log Log { get; }

        public PictureBoxIpl PicBox { get; }

        #endregion

        #region Methods

        public Task EvaluateAsync()
        {
            return Task.Run(() =>
            {
                _cts = new CancellationTokenSource();

                _evaluationController.Evaluate(_progress, _cts.Token);
            });
        }

        #endregion

        #region EventHandlers

        private async void OnButtonOKClickAsync(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            btnOK.Enabled = false;

            await EvaluateAsync();

            progressBar.Visible = false;
            btnOK.Enabled = true;
        }

        private void OnButtonOpenFolderClick(object sender, EventArgs e)
        {
            using (var folderDlg = new FolderBrowserDialog())
            {
                folderDlg.Description = "Выберите каталог";

                if(folderDlg.ShowDialog(this) == DialogResult.OK)
                {
                    _evaluationController.Folder = folderDlg.SelectedPath;
                    tboxFolder.Text = folderDlg.SelectedPath;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MinimumSize = Size;
            Font = SystemFonts.MessageBoxFont;
        }

        #endregion

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }
    }
}
