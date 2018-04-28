﻿using OpenCvSharp;

using PlateDetector.Detection;
using PlateDetector.Logging;

using System;
using System.Drawing;
using System.Windows.Forms;


namespace PlateDetector.UI
{
	public partial class MainForm : Form
	{
		#region Data
		/// <summary> Контроллер лога. </summary>
		private LogController _logController;

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

		/// <summary> Лог.</summary>
		public Log Log { get; private set; }

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

			_detector.Detected	  += OnDetected;
			_detector
				.Manager
				.AlgorithmChanged += OnAlgorithmChanged;
			Log.LogFileUpdated	  += OnLogFileUpdated;
			FormClosing			  += OnFormClosing;

			Log.Info($"Выбран алгоритм: {_detector.SelectedAlgorithm}");
		}

		private void OnAlgorithmChanged(object sender, AlgChangeEventArgs e)
		{
			Log.Info($"Выбран алгоритм: {e.SelectedAlgorithm}");
		}

		private void Detect()
		{
			_detector.Image = pictureBox.ImageIpl;
			_detector.Detect();
		}
		#endregion

		#region EventHandlers

		private void OnDetected(object sender, DetectionEventArgs e)
		{
			foreach(var detection in e.Detections.GetDetectionsList())
			{
				var region = detection.Region;
				_detector
					.Image
					.AddRectangle(region);

				Log.Detection(detection);
			}

			var ms = e.Detections.ElapsedTime.Milliseconds + 1000 * e.Detections.ElapsedTime.Seconds;
			Log.Info($"Время: {ms / 1000f} сек");

			pictureBox.ImageIpl = _detector.Image;
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			_detector.Detected	  -= OnDetected;
			_detector
				.Manager
				.AlgorithmChanged -= OnAlgorithmChanged;
			Log.LogFileUpdated	  -= OnLogFileUpdated;
			FormClosing			  -= OnFormClosing;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize	= Size;
			BackColor	= SystemColors.Window;
			Location	= new System.Drawing.Point(200, 0);
			Font		= SystemFonts.MessageBoxFont;
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

		private void OnLoadImgToolStripMenuItemClick(object sender, EventArgs e)
		{
			using(var openFileDlg = new OpenFileDialog())
			{
				openFileDlg.Title  = "Выберите картинку";
				openFileDlg.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|All files|*.*";
				//openFileDlg.InitialDirectory = Path.Combine(
				//	Directory.GetCurrentDirectory(),
				//	"Photo");

				if(openFileDlg.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						pictureBox.ImageIpl = new Mat(openFileDlg.FileName);

						Log.Info($"Загружено изображение: {openFileDlg.FileName}");

					}
					catch (Exception exc)
					{
						Log.Error(exc.Message);
					}
				}			
			}
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
		#endregion

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
	}
}
