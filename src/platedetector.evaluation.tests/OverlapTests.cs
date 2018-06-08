using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenCvSharp;

namespace Platedetector.Evaluation.Tests
{
    [TestClass]
    public class OverlapTests
    {
        [TestMethod]
        public void IoU_IoUBetweenSame2x2x2x2And2x2x2x2_Returns1()
        {
            //arrange
            var rect1 = new Rect(2, 2, 2, 2);
            var rect2 = new Rect(2, 2, 2, 2);
            var expectedIoU = 1.0;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: float.Epsilon);
        }

        [TestMethod]
        public void IoU_IoUBetweenNoIntersects5x5x20x10And10x20x12x14_Returns0()
        {
            //arrange
            var rect1 = new Rect(5, 5, 20, 10);
            var rect2 = new Rect(10, 20, 12, 14);
            var expectedIoU = 0.0;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: float.Epsilon);
        }

        [TestMethod]
        public void IoU_IoUBetween5x5x5x5And7x7x7x5_Returns0point1765()
        {
            //arrange
            var rect1 = new Rect(5, 5, 5, 5);
            var rect2 = new Rect(7, 7, 7, 5);
            var expectedIoU = 0.1765;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: 0.00005);
        }

        [TestMethod]
        public void IoU_IoUBetween7x7x7x5And5x5x5x5_Returns0point1765()
        {
            //arrange
            var rect1 = new Rect(7, 7, 7, 5);
            var rect2 = new Rect(5, 5, 5, 5);
            var expectedIoU = 0.1765;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: 0.00005);
        }

        [TestMethod]
        public void IoU_IoUBetweenEmbedded7x7x7x5And6x6x10x7_Returns0point5()
        {
            //arrange
            var rect1 = new Rect(7, 7, 7, 5);
            var rect2 = new Rect(6, 6, 10, 7);
            var expectedIoU = 0.5;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: float.Epsilon);
        }

        [TestMethod]
        public void IoU_IoUBetweenTangents11x5x7x8And17x5x4x4_Returns0point0588()
        {
            //arrange
            var rect1 = new Rect(11, 5, 7, 8);
            var rect2 = new Rect(17, 5, 4, 4);
            var expectedIoU = 0.0588;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: 0.00005);
        }

        [TestMethod]
        public void IoU_IoUBetweenTangents6x9x6x4And11x7x3x6_Returns0point1053()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var expectedIoU = 0.1053;

            //act
            var resultIoU = Overlap.IoU(rect1, rect2);


            Assert.AreEqual(expectedIoU, resultIoU, delta: 0.00005);
        }

        [TestMethod]
        public void IoU_IoUBetweenListsSize0And2_ReturnsEmptyList()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var predicted = new List<Rect> { rect1, rect2 };
            var grountTruth = new List<Rect>();

            var expectedresult = new List<double[]>();

            //act
            var resultIoU = Overlap.IoU(grountTruth, predicted);


            Assert.AreEqual(expectedresult.Count, resultIoU.Count);
        }

        [TestMethod]
        public void IoU_IoUBetweenListsSize2And0_ReturnsListSizeOf2AndEmptyArrayElements()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var predicted = new List<Rect>();
            var grountTruth = new List<Rect> { rect1, rect2 };

            var expectedresult = new List<double[]> {
                new double[] { },
                new double[] { },
            };

            //act
            var resultIoU = Overlap.IoU(grountTruth, predicted);


            Assert.AreEqual(expectedresult.Count, resultIoU.Count);
            for(int i = 0; i < expectedresult.Count; i++)
            {
                Assert.ThrowsException<IndexOutOfRangeException>(() =>
                {
                    var d = expectedresult[i][0];
                });
            }
        }

        [TestMethod]
        public void IoU_IoUBetweenListsSize1And1_ReturnsListSizeOf1WithElement()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var predicted = new List<Rect> { rect1 };
            var grountTruth = new List<Rect> { rect2 };

            var expectedresult = new List<double[]> {
                new double[] { 0.1053 },
            };

            //act
            var resultIoU = Overlap.IoU(grountTruth, predicted);


            Assert.AreEqual(expectedresult.Count, resultIoU.Count);
            Assert.AreEqual(expectedresult[0][0], resultIoU[0][0], delta: 0.00005);
        }

        [TestMethod]
        public void IoU_IoUBetweenListsSize1And2_ReturnsListSizeOf1With2Elements()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var rect3 = new Rect(7, 7, 7, 5);
            var predicted = new List<Rect> { rect1, rect2 };
            var grountTruth = new List<Rect> { rect3 };

            var expectedresult = new List<double[]> {
                new double[] { 0.3409, 0.3947 },
            };

            //act
            var resultIoU = Overlap.IoU(grountTruth, predicted);


            Assert.AreEqual(expectedresult.Count, resultIoU.Count);
            Assert.AreEqual(expectedresult[0].Length, resultIoU[0].Length);
            for (int i = 0; i < expectedresult[0].Length; i++)
            {
                Assert.AreEqual(expectedresult[0][i], resultIoU[0][i], delta: 0.00005);
            }
        }

        [TestMethod]
        public void IoU_IoUBetweenListsSize3And2_ReturnsListSizeOf3With2Elements()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var rect3 = new Rect(7, 7, 7, 5);
            var rect4 = new Rect(5, 5, 20, 10);
            var rect5 = new Rect(10, 20, 12, 14);

            var predicted = new List<Rect> { rect1, rect2 };
            var grountTruth = new List<Rect> { rect3, rect4, rect5 };

            var expectedresult = new List<double[]> {
                new double[] { 0.3409, 0.3947 },
                new double[] { 0.12, 0.09 },
                new double[] { 0.0, 0.0 },
            };

            //act
            var resultIoU = Overlap.IoU(grountTruth, predicted);


            Assert.AreEqual(expectedresult.Count, resultIoU.Count);
            Assert.AreEqual(expectedresult[0].Length, resultIoU[0].Length);
            for (int i = 0; i < expectedresult.Count; i++)
            {
                for(int j = 0; j < expectedresult[i].Length; j++)
                Assert.AreEqual(expectedresult[i][j], resultIoU[i][j], delta: 0.00005);
            }
        }

        [TestMethod]
        public void NumTruePositives_TruePositivesBetweenListsSize2And3WithAllIoULowerThanThreshold_Returns0()
        {
            //arrange
            var rect1 = new Rect(6, 9, 6, 4);
            var rect2 = new Rect(11, 7, 3, 6);
            var rect3 = new Rect(7, 7, 7, 5);
            var rect4 = new Rect(5, 5, 20, 10);
            var rect5 = new Rect(10, 20, 12, 14);

            var predicted = new List<Rect> { rect1, rect2 };
            var grountTruth = new List<Rect> { rect3, rect4, rect5 };
            var expectedResult = 0;

            //act
            var result = Overlap.NumTruePositives(predicted, grountTruth);


            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void NumTruePositives_TruePositivesBetweenListsSize2And3With1IoUGreaterThanThreshold_Returns1()
        {
            //arrange
            var rect1 = new Rect(6, 6, 10, 7);
            var rect2 = new Rect(11, 7, 3, 6);
            var rect3 = new Rect(7, 7, 7, 5);
            var rect4 = new Rect(5, 5, 20, 10);
            var rect5 = new Rect(10, 20, 12, 14);

            var predicted = new List<Rect> { rect1, rect2 };
            var grountTruth = new List<Rect> { rect3, rect4, rect5 };
            var expectedResult = 1;

            //act
            var result = Overlap.NumTruePositives(predicted, grountTruth);


            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void NumTruePositives_TruePositivesBetweenListsSize2And3WithAllIoUsGreaterThanThreshold_Returns3()
        {
            //arrange
            var rect1 = new Rect(2, 2, 2, 2);
            var rect2 = new Rect(2, 2, 2, 2);
            var rect3 = new Rect(2, 2, 2, 2);
            var rect4 = new Rect(2, 2, 2, 2);
            var rect5 = new Rect(2, 2, 2, 2);

            var predicted = new List<Rect> { rect1, rect2 };
            var grountTruth = new List<Rect> { rect3, rect4, rect5 };
            var expectedResult = 3;

            //act
            var result = Overlap.NumTruePositives(predicted, grountTruth);


            Assert.AreEqual(expectedResult, result);
        }
    }
}
