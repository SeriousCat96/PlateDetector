using OpenCvSharp;
using OpenCvSharp.UserInterface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateDetector.Detection;
using PlateDetector.Markup;
using PlateDetector.Logging;

namespace PlateDetector.UI
{
	public sealed class DetectionController
	{
		#region .ctor
		public DetectionController(PictureBoxIpl picBox, Log log)
		{
			PicBox				= picBox;
			Image				= picBox.ImageIpl;

			Log					= log;
		}

		#endregion
		
		#region Properties

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

		public void Draw(DetectionResult detections)
		{
			if(detections.GetDetectionsList().Count > 0)
			{
				foreach(var detection in detections.GetDetectionsList())
				{
					var region = detection.Region;

					Image.AddRectangle(region, RegionColor);
					
					Log.Detection(detection);
				}

				var ms = detections.ElapsedTime.Milliseconds + 1000 * detections.ElapsedTime.Seconds;
				Log.Info($"Время: {ms / 1000f} сек");
			}

			PicBox.RefreshIplImage(Image);
		}

		#endregion
	}
}
