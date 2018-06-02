using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp.UserInterface;
using Platedetector.Controllers;

namespace Platedetector.Tests
{
    [TestClass]
    public class ImageSwitchcontrollerTests
    {
        [TestMethod]
        public void MoveTo_ChangeCurPositionFromPosition2To0WithItemsCount5_CurPositionIs0()
        {
            //arrange
            var startPos = 2;
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = controller.Items[startPos];

            Type t = typeof(ImageSwitchController);
            int positionExpected = 0;

            // act
            t.InvokeMember(
                "MoveTo",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null,
                controller,
                new object[] { positionExpected });

            var curPosition = controller.CurPosition;


            Assert.AreEqual(positionExpected, curPosition);
        }

        [TestMethod]
        public void MoveTo_ChangeCurPositionFromPosition1To4WithItemsCount5_CurPositionIs4()
        {
            //arrange
            var startPos = 1;
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = controller.Items[startPos];

            Type t = typeof(ImageSwitchController);
            int positionExpected = 4;

            // act
            t.InvokeMember(
                "MoveTo",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null,
                controller,
                new object[] { positionExpected });

            var curPosition = controller.CurPosition;


            Assert.AreEqual(positionExpected, curPosition);
        }

        [TestMethod]
        public void MoveTo_ChangeCurPositionFromPosition3To5WithItemsCount5_CurPositionIs0()
        {
            //arrange
            var startPos = 3;
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = controller.Items[startPos];

            Type t = typeof(ImageSwitchController);
            int position = 5;
            int positionExpected = 0;

            // act
            t.InvokeMember(
                "MoveTo",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null,
                controller,
                new object[] { position });

            var curPosition = controller.CurPosition;


            Assert.AreEqual(positionExpected, curPosition);
        }

        [TestMethod]
        public void MoveTo_ChangeCurPositionFromPosition3ToMinus1WithItemsCount5_CurPositionIs4()
        {
            //arrange
            var startPos = 3;
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = controller.Items[startPos];

            Type t = typeof(ImageSwitchController);
            int position = -1;
            int positionExpected = 4;

            // act
            t.InvokeMember(
                "MoveTo",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null,
                controller,
                new object[] { position });

            var curPosition = controller.CurPosition;


            Assert.AreEqual(positionExpected, curPosition);
        }

        [TestMethod]
        public void MoveNext_MoveFromFileABH1ToABH2_FileIsABH2()
        {
            //arrange
            var startFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH1.jpg");
            var expectedFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH2.jpg");
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = startFile;

            //act
            controller.MoveNext();
            var resultFile = controller.DataProvider.File;


            Assert.AreEqual(expectedFile, resultFile);
        }

        [TestMethod]
        public void MoveNext_MoveFromFileABH5ToABH1_FileIsABH1()
        {
            //arrange
            var startFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH5.jpg");
            var expectedFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH1.jpg");
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = startFile;

            //act
            controller.MoveNext();
            var resultFile = controller.DataProvider.File;


            Assert.AreEqual(expectedFile, resultFile);
        }

        [TestMethod]
        public void MoveBack_MoveFromFileABH5ToABH4_FileIsABH4()
        {
            //arrange
            var startFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH5.jpg");
            var expectedFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH4.jpg");
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = startFile;

            //act
            controller.MoveBack();
            var resultFile = controller.DataProvider.File;


            Assert.AreEqual(expectedFile, resultFile);
        }

        [TestMethod]
        public void MoveBack_MoveFromFileABH1ToABH5_FileIsABH5()
        {
            //arrange
            var startFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH1.jpg");
            var expectedFile = Path.Combine(Directory.GetCurrentDirectory(), @"Images\ABH5.jpg");
            var controller = new ImageSwitchController(new PictureBoxIpl())
            {
                Items = Directory
                    .EnumerateFiles(@"Images")
                    .ToList()
            };
            controller.DataProvider.File = startFile;

            //act
            controller.MoveBack();
            var resultFile = controller.DataProvider.File;


            Assert.AreEqual(expectedFile, resultFile);
        }
    }
}
