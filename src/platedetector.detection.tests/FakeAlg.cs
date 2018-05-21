using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace Platedetector.Detection.Tests
{
    public class FakeAlgProvider : IDetectionAlgProvider
    {
        public IDetectionAlg CreateDetectionAlgorithm()
        {
            return new FakeAlg();
        }
    }

    public class FakeAlg : IDetectionAlg
    {
        public DetectionResultPattern Pattern => DetectionResultPattern.RegionOnly;

        public void Load(string filename) { }

        public IReadOnlyList<Detection> Predict(Mat image) => new List<Detection>
        {
            new Detection(new Rect(5, 10, 100, 20)),
        };
    }
}
