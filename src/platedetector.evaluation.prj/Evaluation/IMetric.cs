using OpenCvSharp;
using System.Collections.Generic;

namespace PlateDetector.Evaluation
{
    public interface IMetric
	{
        int NumTruePositive { get; set; }

        double Compute();

        void Stash(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted);
	}
}
