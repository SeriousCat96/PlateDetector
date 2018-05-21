using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Platedetector.Evaluation.Tests
{
    [TestClass]
    public class PrecisionMetricTests
    {
        [TestMethod]
        public void Compute_NumDetections9AndNumTruePositives9_Returns1()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 9,
                NumTruePositive = 9
            };
            var expectedResult = 1.0;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }

        [TestMethod]
        public void Compute_NumDetections5AndNumTruePositives0_Returns0()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 5,
                NumTruePositive = 0
            };
            var expectedResult = 0.0;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }

        [TestMethod]
        public void Compute_NumDetections20AndNumTruePositives10_Returns0Point5()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 20,
                NumTruePositive = 10
            };
            var expectedResult = 0.5;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }

        [TestMethod]
        public void Compute_NumDetections5AndNumTruePositives4_Returns0Point8()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 5,
                NumTruePositive = 4
            };
            var expectedResult = 0.8;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }
    }
}
