using OpenCvSharp;

using Platedetector.Controllers;
using Platedetector.Detection;
using Platedetector.Detection.Core;
using Platedetector.Drawing;
using Platedetector.Markup;
using Platedetector.Utils.Logging;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace Platedetector.UI
{
    public partial class MainForm : Form
	{
		#region Data

		/// <summary> Контроллер детектирования. </summary>
		private DetectionController _detectionController;

		/// <summary> Контроллер разметки. </summary>
		private MarkupController _markupController;

		/// <summary> Контроллер навигации по изображениям. </summary>
		private ImageSwitchController _imageController;

		/// <summary> Реализует детектор номеров. </summary>
		private Detector _detector;

		#endregion

		#region .ctor

		public MainForm()
		{
			InitializeComponent();
            InitializeData();
		}

		#endregion

		#region Properties

		/// <summary> Лог.</summary>
		public Log Log { get; private set; }

		/// <summary> Оригинал изображения. </summary>
		public Mat OriginalImage { get; private set; }

		#endregion

		#region Methods

		/// <summary> Инициализирует поля.</summary>
		private void InitializeData()
		{
			_detector		= new Detector(
				new AlgManager(
					new FasterRcnnRpnProvider(),
					new HaarCascadeProvider()
					)
				);

			Log				= new Log();

			_detectionController  = new DetectionController(pictureBox, Log);
			_markupController	  = new MarkupController(pictureBox, Log);
			_imageController	  = new ImageSwitchController(pictureBox);

			_detector.Detected	   += OnDetected;
			_detector
				.Manager
				.AlgorithmChanged  += OnAlgorithmChanged;

			Log.LogFileUpdated	   += OnLogFileUpdated;

			_markupController
				.MarkupModeChanged += OnMarkupModeChanged;

			_imageController
				.DataProvider
				.FileChanged	   += OnFileChanged;

			FormClosing			   += OnFormClosing;

			tboxAlg.Text = _detector
				.SelectedAlgorithm
				?.ToString();
		}	

		private void Detect()
		{
			_detector.Detect(OriginalImage);
		}

		private void MoveNext()
		{
			try
			{
				_imageController.MoveNext();

				tboxFolder.Text = _imageController
							.DataProvider
							.Folder;

				ShowMarkup();
			}
			catch(FileNotFoundException exc)
			{
				Log.Warning(exc.Message);
			}
			catch(InvalidOperationException exc)
			{
				Log.Warning(exc.Message);
			}
            catch(NullReferenceException)
            {
                Log.Error("Изображение неопределено");
            }
            catch(Exception exc)
			{
				Log.Error(exc.Message);
			}
		}

		private void MoveBack()
		{
			try
			{
				_imageController.MoveBack();

				tboxFolder.Text = _imageController
							.DataProvider
							.Folder;

				ShowMarkup();
			}
			catch(FileNotFoundException exc)
			{
				Log.Warning(exc.Message);
			}
			catch(InvalidOperationException exc)
			{
				Log.Warning(exc.Message);
			}
            catch(NullReferenceException)
            {
                Log.Error("Изображение неопределено");
            }
            catch(Exception exc)
			{
				Log.Error(exc.Message);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if(keyData == Keys.Left)
			{
				MoveBack();
				return true;
			}

			if(keyData == Keys.Right)
			{
				MoveNext();
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void ShowMarkup()
		{
			var uri = _imageController
				.DataProvider
				.File;

            Log.Info($"Загружено изображение: {uri}");

			_markupController.OriginalImage = pictureBox.ImageIpl;
			_markupController.Draw(uri);

            OriginalImage = _markupController.OriginalImage;
        }

        private void WriteStartupMessages()
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblyName = assembly.GetName();
            var computerInfo = new ComputerInfo();
            var cpu = ((Registry
                .LocalMachine
                .OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree)
                .GetValue("ProcessorNameString") ?? "Unknown") as string)
                .Replace("(R)", "®")
                .Replace("(TM)", "™");

            Log.Info($"Application \"{assemblyName.Name} {assemblyName.Version}\" started");
            Log.Info($"OS: {computerInfo.OSFullName}");
            Log.Info($"RAM: {computerInfo.TotalPhysicalMemory / (1024 * 1024)} MB");
            Log.Info($"CPU: {cpu}");
            Log.Info($"Выбран алгоритм: {_detector.SelectedAlgorithm}");
        }

		#endregion

		#region EventHandlers

		private void OnAlgorithmChanged(object sender, AlgChangedEventArgs e)
		{
			Log.Info($"Выбран алгоритм: {e.SelectedAlgorithm}");

			tboxAlg.Text = e
				.SelectedAlgorithm
				.ToString();
		}

		private void OnButtonDetectClick(object sender, EventArgs e)
		{
			try
			{
				Detect();
			}
            catch(ArgumentNullException exc)
            {
                switch (exc.ParamName)
                {
                    case "algorithm":
                        Log.Error("Алгоритм неопределен");
                        break;
                    case "image":
                        Log.Error("Изображение неопределено");
                        break;
                }
            }
            catch (Exception exc)
			{
				Log.Error(exc.Message);
			}
		}

		private void OnButtonMoveNextClick(object sender, EventArgs e)
		{
			MoveNext();
		}

		private void OnButtonMoveBackClick(object sender, EventArgs e)
		{
			MoveBack();
		}

		private void OnButtonOpenFolderClick(object sender, EventArgs e)
		{
			Process.Start("explorer", _imageController.DataProvider.Folder);
		}

		private void OnCheckboxMarkupCheckedChanged(object sender, EventArgs e)
		{
			_markupController.IsMarkupOn = chkBoxMarkup.Checked;
		}

		private void OnChooseAlgToolStripMenuItemClick(object sender, EventArgs e)
		{
			using(var window = new AlgForm(_detector.Manager))
			{
				window.ShowDialog(this);
			}
		}

		private void OnDetected(object sender, DetectionEventArgs e)
		{
			var uri = _imageController
				.DataProvider
				.File;

			try
			{
				_markupController.Draw(uri);
			}
			catch { }

			_detectionController.Draw(e.Detections);

            if(_markupController.IsMarkupOn)
            {
                new ImageTextRenderer(pictureBox)
                    .PutIou(e.Detections, _markupController.GtBoxes);
            }
		}

		private void OnEvalAlgToolStripMenuItemClick(object sender, EventArgs e)
		{
            using (var window = new EvalForm(
                new Detector(
                    new AlgManager(
                        new FasterRcnnRpnProvider(),
                        new HaarCascadeProvider())),
                Log,
                _imageController.DataProvider.Folder))
			{
                var selectedAlg = _detector.SelectedAlgorithm;

                window
                    .Detector
                    .ChangeAlgorithm(selectedAlg.GetType());
				window.ShowDialog(this);
			}
		}

		private void OnFileChanged(object sender, FileChangedEventArgs e)
		{
			_detectionController.RefreshDetections();
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			_detector.Detected	   -= OnDetected;
			_detector
				.Manager
				.AlgorithmChanged  -= OnAlgorithmChanged;

			Log.LogFileUpdated	   -= OnLogFileUpdated;

			_markupController
				.MarkupModeChanged -= OnMarkupModeChanged;

			_imageController
				.DataProvider
				.FileChanged	   -= OnFileChanged;

			FormClosing			   -= OnFormClosing;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize	= Size;
			Location	= new System.Drawing.Point(200, 0);
			Font		= SystemFonts.MessageBoxFont;

            WriteStartupMessages();
        }

		private void OnLoadImgToolStripMenuItemClick(object sender, EventArgs e)
		{
			using(var openFileDlg = new OpenFileDialog())
			{
				openFileDlg.Title = "Выбор изображения";
				openFileDlg.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|All files|*.*";

				if(openFileDlg.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						string uri = openFileDlg.FileName;

						_imageController
							.DataProvider
							.File = uri;

						tboxFolder.Text = _imageController
							.DataProvider
							.Folder;

						ShowMarkup();
					}
					catch(FileNotFoundException exc)
					{
						Log.Warning(exc.Message);
					}
					catch(InvalidOperationException exc)
					{
						Log.Warning(exc.Message);
					}
					catch(Exception exc)
					{
						Log.Error(exc.Message);
					}
				}
			}
		}

		private void OnLogFileUpdated(object sender, LogEventArgs e)
		{
			MethodInvoker mi = new MethodInvoker(() =>
			{
				if(logView.Items.Count == logView.RecordLimit)
				{
                    logView
						.Items
						.Clear();
				}
                logView
                    .Items
                    .Add(new LogItem(e.Message));
                logView.TopIndex = logView
                    .Items
                    .Count - 1;
            });

			if(logView.InvokeRequired)
			{
				if(!IsDisposed)
				{
					try
					{
                        logView.BeginInvoke(mi);
					}
					catch(ObjectDisposedException)
					{
					}
				}
			}
			else
			{
				mi();
			}
		}

		private void OnMarkupModeChanged(object sender, EventArgs e)
		{
			var mode = chkBoxMarkup.Checked ? "включена" : "отключена";

			Log.Info($"Синхронизация с разметкой {mode}");
			try
			{
				var uri = _imageController
					.DataProvider
					.File;

				_markupController.Draw(uri);
				_detectionController.Draw();

                if (_markupController.IsMarkupOn)
                {
                    new ImageTextRenderer(pictureBox)
                        .PutIou(_detectionController.Detections, _markupController.GtBoxes);
                }
            }
			catch(FileNotFoundException exc)
			{
				Log.Warning(exc.Message);
			}
			catch(InvalidOperationException exc)
			{
				Log.Warning(exc.Message);
			}
            catch(NullReferenceException)
            {
                Log.Error("Изображение неопределено");
            }
            catch(Exception exc)
			{
				Log.Error(exc.Message);
			}
		}
		#endregion
	}
}
