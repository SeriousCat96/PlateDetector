using Platedetector.Controllers;
using Platedetector.Detection;
using Platedetector.Evaluation;
using Platedetector.Utils.Logging;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platedetector.UI
{
    public partial class EvalForm : Form
	{
        #region Data

        private EvaluationController _evaluationController;

        private Progress<ProgressReport> _progress;

        private CancellationTokenSource _cts;

        #endregion
        
        #region .ctor

        public EvalForm(Detector detector, Log log, string initFolder)
        {
            InitializeComponent();
            
            Detector = detector;
            Log = log;

            _evaluationController = new EvaluationController(
                new List<IMetric>
                {
                    new RecallMetric(),
                    new PrecisionMetric()
                },
                Detector,
                Log)
            {
                Folder = initFolder
            };

            IsRunning = false;

            SetViewProperties();
        }

        #endregion

        #region Properties

        public Detector Detector { get; private set; }

        public Log Log { get; }

        public bool IsRunning { get; set; }

        #endregion

        #region Methods

        private void SetViewProperties()
        {
            listViewMetrics
                .Items
                .AddRange(_evaluationController.Metrics.Select(e => new ListViewItem(e.ToString())).ToArray());

            _progress = new Progress<ProgressReport>();
            _progress.ProgressChanged += (s, args) =>
            {
                progressBar.Maximum = args.ItemsCount;
                progressBar.Value = args.CurPosition;
                tboxFileName.Text = args.File;
            };

            progressBar.Style = ProgressBarStyle.Continuous;

            tboxFolder.Text = _evaluationController.Folder;
        }

        private async Task EvaluateAsync()
        {
            if (Detector.SelectedAlgorithm == null)
            {
                Log.Error("Алгоритм не определен.");
                return;
            }

            _cts = new CancellationTokenSource();
            IsRunning = true;

            await Task.Run(
                async () =>
                {
                    await _evaluationController.EvaluateAsync(_progress, _cts.Token);
                },
                _cts.Token);
        }
        #endregion

        #region EventHandlers
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if(IsRunning)
            {
                var result = MessageBox.Show(
                this,
                "Вы уверены что хотите закрыть окно? Процесс оценки алгоритма будет остановлен",
                "",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

                switch (result)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.OK:
                        _cts?.Cancel();
                        break;
                }
            }
            else
            {
                _cts?.Cancel();
            }
        }

        private void OnButtonOpenFolderClick(object sender, EventArgs e)
        {
            using (var folderDlg = new FolderBrowserDialog())
            {
                folderDlg.ShowNewFolderButton = false;
                folderDlg.SelectedPath = _evaluationController.Folder;
                folderDlg.Description = "Выбор каталога";

                if(folderDlg.ShowDialog(this) == DialogResult.OK)
                {
                    _evaluationController.Folder = folderDlg.SelectedPath;
                    tboxFolder.Text = folderDlg.SelectedPath;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MinimumSize = Size;
            Font = SystemFonts.MessageBoxFont;

            tboxAlg.Text = Detector
                .SelectedAlgorithm?
                .ToString();
        }

        private async void OnButtonStartClickAsync(object sender, EventArgs e)
        {
            groupBox.Visible = true;
            btnStop.Enabled  = true;
            btnStart.Enabled = false;

            await EvaluateAsync();

            IsRunning = false;
            groupBox.Visible = false;
            btnStop.Enabled  = false;
            btnStart.Enabled = true;
        }

        private void OnStopButtonClick(object sender, EventArgs e) => _cts?.Cancel();

        #endregion

    }
}
