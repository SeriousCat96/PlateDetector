using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace Platedetector.Detection.Tests
{
    public class DummyAlgProvider : IDetectionAlgProvider
    {
        public IDetectionAlg CreateDetectionAlgorithm()
        {
            return new DummyAlg();
        }
    }

    public class DummyAlg : IDetectionAlg
    {
        public DetectionResultPattern Pattern => DetectionResultPattern.RegionWithProbabilityAndCountry;

        public void Load(string filename) { }

        public IReadOnlyList<Detection> Predict(Mat image) => new List<Detection>
        {
            new Detection(new Rect(15, 7, 34, 11), 0.99f, Country.Russia),
            new Detection(new Rect(1, 1, 30, 8), 0.99f, Country.Ukraine),
        };
}
}
