using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Platedetector.Evaluation.Tests
{
    [TestClass]
    public class PrecisionMetricTests
    {
        [TestMethod]
        public void Compute_NumDetections20AndNumTruePositives20_Returns1()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 20,
                NumTruePositive = 20
            };
            var expectedResult = 1.0;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }

        [TestMethod]
        public void Compute_NumDetections20AndNumTruePositives0_Returns0()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 20,
                NumTruePositive = 0
            };
            var expectedResult = 0.0;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }

        [TestMethod]
        public void Compute_NumDetections20AndNumTruePositives1_Returns0Point05()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 20,
                NumTruePositive = 1
            };
            var expectedResult = 0.05;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }

        [TestMethod]
        public void Compute_NumDetections20AndNumTruePositives19_Returns0Point95()
        {
            //arrange
            var metric = new PrecisionMetric
            {
                NumDetections = 20,
                NumTruePositive = 19
            };
            var expectedResult = 0.95;

            //act
            var result = metric.Compute();


            Assert.AreEqual(expectedResult, result, delta: float.Epsilon);
        }
    }
}
