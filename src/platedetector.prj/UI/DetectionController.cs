using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Detection;
using PlateDetector.Markup;
using PlateDetector.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.UI
{
	public sealed class DetectionController
	{
		#region .ctor
		public DetectionController(PictureBoxIpl picBox, Log log)
		{
			PicBox		= picBox;
			Image		= picBox.ImageIpl;
			Log			= log;

			RefreshDetections();
		}

		#endregion
		
		#region Properties

		public DetectionResult Detections { get; private set; }

		public PictureBoxIpl PicBox { get; }

		public Mat Image
		{
			get
			{
				if(PicBox == null)
				{
					throw new InvalidOperationException("Picture box is not initialized");
				}
				return PicBox.ImageIpl;
			}
			set
			{
				if(PicBox == null)
				{
					throw new InvalidOperationException("Picture box is not initialized");
				}
				PicBox.ImageIpl = value;
			}
		}

		public Log Log { get; private set; }

		public Scalar RegionColor => Scalar.Fuchsia;

		#endregion

		#region Methods

		public void Draw(DetectionResult detections = null)
		{
			if(detections != null)
			{
				Detections = detections;
			}

			if(Detections.GetDetectionsList().Count > 0)
			{
				foreach(var detection in Detections.GetDetectionsList())
				{
					var region = detection.Region;

					Image.AddRectangle(region, RegionColor);
					
					if(detections != null)
					{
						Log.Detection(detection);
					}
				}

				if(detections != null)
				{
					var ms = Detections.ElapsedTime.Milliseconds + 1000 * Detections.ElapsedTime.Seconds;
					Log.Info($"Время: {ms / 1000f} сек");
				}
			}

			PicBox.RefreshIplImage(Image);

			GC.Collect();
		}

		public void RefreshDetections()
		{
			Detections = new DetectionResult(new List<Detection.Detection>(), new TimeSpan());
		}
		#endregion
	}
}
