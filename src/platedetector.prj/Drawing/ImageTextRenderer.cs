using OpenCvSharp;
using OpenCvSharp.UserInterface;

using Platedetector.Detection;
using Platedetector.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platedetector.Drawing
{
    public class ImageTextRenderer
    {
        public ImageTextRenderer(PictureBoxIpl picBox)
        {
            PicBox = picBox;
            Image  = picBox.ImageIpl;
        }

        public Mat Image { get; }
        public PictureBoxIpl PicBox { get; }

        public void PutIou(DetectionResult detectionResult, IEnumerable<Rect> groundTruth)
        {
            if(groundTruth == null || detectionResult == null)
            {
                return;
            }

            var predicted = detectionResult
                .GetDetectionsList()
                .Select(e => e.Region)
                .ToList();

            if(predicted.Count == 0)
            {
                return;
            }

            var gt = groundTruth.ToList();
            var ious = Overlap.Iou(gt, predicted);

            for (int i = 0; i < ious.Count; i++)
            {
                var maxIou = ious[i].Max();
                var iou = Math.Round(maxIou, 5);
                var idx = ious[i]
                    .ToList()
                    .IndexOf(maxIou);
                var pointLocation = gt[i].Location;

                pointLocation.X += 10;
                pointLocation.Y += 15;

                var picboxSize = new Size(PicBox.Width, PicBox.Height);
                var imageSize  = Image.Size();
                var factor = ((float)picboxSize.Width / imageSize.Width +
                                (float)picboxSize.Height / imageSize.Height) * 0.5;
                var thickness = (int)Math.Round(1.2 / factor);

                Image.PutText(
                    text: $"IoU: {iou}",
                    org: pointLocation,
                    fontFace: HersheyFonts.HersheyDuplex,
                    fontScale: 0.5 / factor,
                    color: Scalar.Red,
                    thickness: thickness);
            }

            PicBox.RefreshIplImage(Image);
        }
    }
}
