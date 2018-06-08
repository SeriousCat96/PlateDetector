using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace Platedetector.Detection.Tests
{
    [TestClass]
    public class DetectorTests
    {
        [TestMethod]
        public void Detect_DetectObjectsWithDummyAlg_ReturnsRussiaAndUkraineObjects()
        {
            //arrange
            var detector = new Detector(new AlgManager(new DummyAlgProvider()));
            var expectedPattern = DetectionResultPattern.RegionWithProbabilityAndCountry;
            var expectedResult = new DetectionResult(
                new List<Detection>
                {
                    new Detection(new Rect(15, 7, 34, 11), 0.99f, Country.Russia),
                    new Detection(new Rect(1, 1, 30, 8), 0.99f, Country.Ukraine),
                },
                new TimeSpan(),
                expectedPattern);

            //act
            var result = detector.Detect(new Mat(3, 3, MatType.CV_8UC3));

            var expectedRects = expectedResult
                .GetDetectionsList()
                .ToArray();
            var resultRects = result
                .GetDetectionsList()
                .ToArray();

            //assert
            for(int i = 0; i < expectedRects.Length; i++)
            {
                Assert.AreEqual(expectedRects[i].Region, resultRects[i].Region);
                Assert.AreEqual(expectedRects[i].Country, resultRects[i].Country);
                Assert.AreEqual(expectedRects[i].Probability, resultRects[i].Probability, delta: float.Epsilon);
            }
            Assert.AreEqual(expectedResult.Pattern, expectedPattern);
        }

        [TestMethod]
        public void Detect_DetectObjectsWithFakeAlg_Returns1Object()
        {
            //arrange
            var detector = new Detector(new AlgManager(new FakeAlgProvider()));
            var expectedPattern = DetectionResultPattern.RegionOnly;
            var expectedResult = new DetectionResult(
                new List<Detection>
                {
                    new Detection(new Rect(5, 10, 100, 20)),
                },
                new TimeSpan(),
                expectedPattern);

            //act
            var result = detector.Detect(new Mat(3, 3, MatType.CV_8UC3));

            var expectedRects = expectedResult
                .GetDetectionsList()
                .ToArray();
            var resultRects = result
                .GetDetectionsList()
                .ToArray();

            //assert
            for (int i = 0; i < expectedRects.Length; i++)
            {
                Assert.AreEqual(expectedRects[i].Region, resultRects[i].Region);
                Assert.AreEqual(expectedRects[i].Country, Country.None);
                Assert.AreEqual(expectedRects[i].Probability, 0.0, delta: float.Epsilon);
            }
            Assert.AreEqual(expectedResult.Pattern, expectedPattern);
        }

        [TestMethod]
        public void Detect_DetectObjectsWithFakeAlgAndNullImage_ThrowsArgumentNullException()
        {
            //arrange
            var detector = new Detector(new AlgManager(new FakeAlgProvider()));
            

            //act
            var action = new Action(() =>
            {
                Mat mat = null;
                detector.Detect(mat);
            });


            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Detect_DetectObjectsWithNullAlgorithm_ThrowsArgumentNullException()
        {
            //arrange
            var detector = new Detector(new AlgManager());


            //act
            var action = new Action(() =>
            { 
                detector.Detect(new Mat(3, 3, MatType.CV_8UC3));
            });


            Assert.ThrowsException<ArgumentNullException>(action);
        }
    }
}
