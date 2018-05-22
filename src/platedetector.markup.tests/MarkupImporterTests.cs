using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace Platedetector.Markup.Tests
{
    [TestClass]
    public class MarkupImporterTests
    {
        [TestMethod]
        public void CreateIfMarkupFileExists_MarkupFileExists_MarkupIsNotNull()
        {
            var importer = new MarkupImporter();
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", "1.jpg");


            typeof(MarkupImporter).InvokeMember(
                "CreateIfMarkupFileExists",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null,
                importer,
                new object[] { targetFile });


            Assert.IsNotNull(importer.Markup);
        }

        [TestMethod]
        public void CreateIfMarkupFileExists_MarkupFileNotExists_ThrowsFileNotFoundException()
        {
            var importer = new MarkupImporter();
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"NotImages", "text.txt");


            var action = new Action(() =>
            {
                MethodInfo mi = typeof(MarkupImporter).GetMethod(
                    "CreateIfMarkupFileExists",
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(importer, new object[] { targetFile });
            });
            

            Assert.ThrowsException<TargetInvocationException>(action);
        }

        [TestMethod]
        public void ImportRegions_FileExistsHumanCheckedIsTrue_ReturnsRectsNormal()
        {
            var importer = new MarkupImporter();
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", "1.jpg");
            var expectedResult = new List<Rect>
            {
                new Rect(369, 707, 243, 51),
            };


            var result = importer
                .ImportRegions(targetFile)
                .ToArray();


            Assert.AreEqual(expectedResult.Count, result.Length);
            Assert.AreEqual(expectedResult[0], result[0]);
        }

        [TestMethod]
        public void ImportRegions_FileExistsHumanCheckedIsFalse_ThrowsInvalidOperationException()
        {
            var importer = new MarkupImporter();
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", "3.jpg");


            var action = new Action(() =>
            {
                var result = importer
                    .ImportRegions(targetFile)
                    .ToArray();
            });


            Assert.ThrowsException<InvalidOperationException>(action);
        }

        [TestMethod]
        public void ImportRegions_FileNotExists_ThrowsFileNotFoundException()
        {
            var importer = new MarkupImporter();
            var targetFile = @"dummy.jpg";


            var action = new Action(() =>
            {
                var result = importer
                    .ImportRegions(targetFile)
                    .ToArray();
            });


            Assert.ThrowsException<FileNotFoundException>(action);
        }
    }
}
