using OpenCvSharp;
using OpenCvSharp.UserInterface;

using Platedetector.Detection;
using Platedetector.Detection.Utils;
using Platedetector.Utils.Logging;

using System;
using System.Collections.Generic;

namespace Platedetector.Controllers
{
    public sealed class DetectionController
    {
        #region .ctor
        public DetectionController(PictureBoxIpl picBox, Log log)
        {
            PicBox = picBox;
            Image = picBox.ImageIpl;
            Log = log;

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
                if (PicBox == null)
                {
                    throw new InvalidOperationException("Picture box is not initialized");
                }
                return PicBox.ImageIpl;
            }
            set
            {
                if (PicBox == null)
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
            if (detections != null)
            {
                Detections = detections;
            }

            if (Detections.GetDetectionsList().Count > 0)
            {
                foreach (var detection in Detections.GetDetectionsList())
                {
                    var region = detection.Region;

                    Image.AddRectangle(region, RegionColor, new Size(PicBox.Width, PicBox.Height));

                    if (detections != null)
                    {
                        Log.Detection(detection);
                    }
                }

                if (detections != null)
                {
                    Log.Info($"Время: {Detections.ElapsedTime.TotalSeconds} сек");
                }
            }

            PicBox.RefreshIplImage(Image);

            GC.Collect();
        }

        public void RefreshDetections()
        {
            Detections = new DetectionResult(new List<Detection.Detection>(), new TimeSpan(), DetectionResultPattern.RegionOnly);
        }
        #endregion
    }
}