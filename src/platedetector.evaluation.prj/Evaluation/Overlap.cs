using OpenCvSharp;
using PlateDetector.Detection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.Evaluation
{
    public static class Overlap
    {
        public static double Threshold => 0.5;

        public static double Iou(Rect box1, Rect box2)
        {
            var xmin1 = box1.X;
            var ymin1 = box1.Y;
            var xmax1 = box1.X + box1.Width;
            var ymax1 = box1.Y + box1.Height;

            var xmin2 = box2.X;
            var ymin2 = box2.Y;
            var xmax2 = box2.X + box1.Width;
            var ymax2 = box2.Y + box1.Height;

            var xA = Math.Max(xmin1, xmin2);
            var yA = Math.Max(ymin1, ymin2);
            var xB = Math.Min(xmax1, xmax2);
            var yB = Math.Min(ymax1, ymax2);

            // Если нет пересечения, то IoU = 0.
            if (!(xmin1 < xmax2 && xmax1 > xmin2 && ymax1 > ymin2 && ymin1 < ymax2))
            {
                return 0;
            }

            // пересечение
            var intersection = Math.Max((xB - xA + 1.0) * (yB - yA + 1.0), 0);

            // Площади
            var box1Area = (xmax1 - xmin1 + 1) * (ymax1 - ymin1 + 1);
            var box2Area = (xmax2 - xmin2 + 1) * (ymax2 - ymin2 + 1);

            var union = box1Area + box2Area - intersection;

            return intersection / union;
        }

        public static IList<double[]> Iou(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted)
        {
            var numGt = groundTruth.Count();
            var numPred = predicted.Count();
            var result = new List<double[]>();

            for(int gtIdx = 0; gtIdx < numGt; gtIdx++)
            {
                var ious = new double[numPred];

                for (int predIdx = 0; predIdx < numPred; predIdx++)
                {
                    ious[predIdx] = Iou(groundTruth[gtIdx], predicted[predIdx]);
                }

                result.Add(ious);
            }

            return result;
        }

        public static int NumTruePositives(IReadOnlyList<Rect> predicted, IReadOnlyList<Rect> groundTruth)
        {
            if(predicted.Count == 0)
            {
                return 0;
            }

            var ious = Iou(groundTruth, predicted);
            int count = 0;

            foreach (var iouArr in ious)
            {
                var max = iouArr.Max();

                if (max >= Threshold)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
