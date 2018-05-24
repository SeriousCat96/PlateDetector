using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace Platedetector.Markup.Tests
{
    [TestClass]
    public class XmlMarkupTests
    {
        [TestMethod]
        public void Load_FileExists_InitUriAndXmlFileProperties()
        {
            var markup = new XmlMarkup();
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", "1.xml");


            markup.Load(targetFile);


            Assert.AreEqual(targetFile, markup.Uri);
            Assert.IsNotNull(markup.XmlFile);
        }

        [TestMethod]
        public void Load_FileNotExists_ThrowsFileNotFoundException()
        {
            var markup = new XmlMarkup();
            var targetFile = @"dummy.xml";


            var action = new Action(() =>
            {
                markup.Load(targetFile);
            });


            Assert.ThrowsException<FileNotFoundException>(action);
        }

        [TestMethod]
        public void Load_NullUri_ThrowsArgumentNullException()
        {
            var markup = new XmlMarkup();


            var action = new Action(() =>
            {
                markup.Load(null);
            });


            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void GetRegions_MarkupIsNotNull_ReturnRectsNormal()
        { 
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", "1.xml");
            var expectedResult = new List<Rect>
            {
                new Rect(369, 707, 243, 51),
            };
            var markup = new XmlMarkup(targetFile);


            var result = markup
                .GetRegions()
                .ToArray();


            Assert.AreEqual(expectedResult.Count, result.Length);
            Assert.AreEqual(expectedResult[0], result[0]);
        }

        [TestMethod]
        public void GetRegions_MarkupIsNull_ThrowsInvalidOperationException()
        {
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", "1.xml");
            var markup = new XmlMarkup(targetFile);
            var info = typeof(XmlMarkup).GetProperty("XmlFile");
            info.SetValue(markup, null);


            var action = new Action(() =>
            {
                markup.GetRegions();
            });


            Assert.ThrowsException<InvalidOperationException>(action);
        }
    }
}
