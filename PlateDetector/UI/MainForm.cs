using OpenCvSharp;

using PlateDetector.Detection;
using PlateDetector.Logging;
using PlateDetector.Markup;

using System;
using System.Drawing;
using System.Windows.Forms;
using PlateDetector.Imaging;
using System.Diagnostics;
using System.IO;

namespace PlateDetector.UI
{
	public partial class MainForm : Form
	{
		#region Data
		/// <summary> Контроллер лога. </summary>
		private LogController _logController;

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
			_logController	= new LogController(lboxLog);
			_detector		= new Detector(
				new AlgManager(
					new FasterRCNNFactory(),
					new HaarCascadeFactory()
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

			FormClosing			   += OnFormClosing;

			Log.Info($"Выбран алгоритм: {_detector.SelectedAlgorithm}");
			tboxAlg.Text = _detector
				.SelectedAlgorithm
				.ToString();
		}

		private void Detect()
		{
			_detector.Image = OriginalImage.Clone();
			_detector.Detect();
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

			OriginalImage = pictureBox.ImageIpl;

			_markupController.OriginalImage = OriginalImage;
			_markupController.Draw(uri);
		}

		#endregion

		#region EventHandlers

		private void OnAlgorithmChanged(object sender, AlgChangeEventArgs e)
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
			catch(Exception exc)
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
				if(window.ShowDialog(this) == DialogResult.OK)
				{
					Console.WriteLine();
				}
			}
		}

		private void OnDetected(object sender, DetectionEventArgs e)
		{
			var uri = _imageController
				.DataProvider
				.File;

			_markupController.Draw(uri);
			_detectionController.Draw(e.Detections);
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

			FormClosing			   -= OnFormClosing;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize	= Size;
			Location	= new System.Drawing.Point(200, 0);
			Font		= SystemFonts.MessageBoxFont;
			//BackColor	= SystemColors.Window;
		}

		private void OnLoadImgToolStripMenuItemClick(object sender, EventArgs e)
		{
			using(var openFileDlg = new OpenFileDialog())
			{
				openFileDlg.Title = "Выберите картинку";
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
				if(_logController.LogListBox.Items.Count == _logController.RecordLimit)
				{
					_logController
						.LogListBox
						.Items
						.Clear();
				}

				_logController
					.LogListBox
					.Items
					.Add(new LogItem(e.Message));

				_logController
					.LogListBox
					.TopIndex = _logController.LogListBox.Items.Count - 1;
			});

			if(lboxLog.InvokeRequired)
			{
				if(!IsDisposed)
				{
					try
					{
						lboxLog.BeginInvoke(mi);
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
		#endregion
	}
}
