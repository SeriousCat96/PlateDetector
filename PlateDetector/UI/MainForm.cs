using OpenCvSharp;

using PlateDetector.Algorithms;
using PlateDetector.Logging;
using PlateDetector.UI;

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

		public Log Log { get; private set; }

		#region Methods

		private void InitializeData()
		{
			_logController	= new LogController(lboxLog);
			_detector		= new Detector(
				new AlgManager(
					new ConvNeuralNetFactory(),
					new HaarCascadeFactory()
					)
				);

			Log				= new Log();

			_detector.Detected	+= OnDetected;
			Log.LogFileUpdated	+= OnLogFileUpdated;
			FormClosing			+= OnFormClosing;

			Log.Info($"Выбран алгоритм: {_detector.SelectedAlgorithm.GetType().Name}");
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
			foreach(var rect in e.Result.GetBoundBoxes())
			{
				_detector
					.Image
					.Rectangle(rect, Scalar.OrangeRed, thickness: 2);

				Log.Detection($"Время: {e.Time.Milliseconds} мс", rect);
			}

			Log.Info($"Время: {e.Time.Milliseconds} мс");

			pictureBox.ImageIpl = _detector.Image;
			
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			_detector.Detected	-= OnDetected;
			Log.LogFileUpdated	-= OnLogFileUpdated;
			FormClosing			-= OnFormClosing;
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

		#endregion

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
	}
}
