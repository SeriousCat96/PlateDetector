using OpenCvSharp;
using System.Collections.Generic;

namespace PlateDetector.Evaluation
{
    public sealed class RecallMetric : IMetric
    {
        public RecallMetric()
        {
            NumTruePositive = 0;
            NumGtBoxes      = 0;
        }

        public int NumGtBoxes { get; set; }

        public int NumTruePositive { get; set; }

        public double Compute()
        {
            var r = (double)NumTruePositive / NumGtBoxes;

            return r;
        }

        public void Stash(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted)
        {
            var numTruePositive = Overlap.NumTruePositives(predicted, groundTruth);

            NumTruePositive += numTruePositive;
            NumGtBoxes      += groundTruth.Count;
        }

        public override string ToString()
        {
            return "Recall";
        }
    }
}
