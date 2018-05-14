using OpenCvSharp;
using System.Collections.Generic;

namespace PlateDetector.Evaluation
{
    public sealed class PrecisionMetric : IMetric
    {
        public PrecisionMetric()
        {
            NumTruePositive = 0;
            NumDetections   = 0;
        }

        public int NumDetections { get; set; }

        public int NumTruePositive { get; set; }

        public double Compute()
        {
            double p = 0.0;

            if (NumDetections > 0)
            {
                p = (double)NumTruePositive / NumDetections;
            }

            return p;
        }

        public void Stash(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted)
        {
            var numTruePositive = Overlap.NumTruePositives(predicted, groundTruth);

            NumTruePositive += numTruePositive;
            NumDetections   += predicted.Count;
        }

        public override string ToString()
        {
            return "Precision";
        }
    }
}
