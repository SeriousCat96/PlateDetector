using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Platedetector.Markup.Tests
{
    [TestClass]
    public class ImageFilesDataProviderTests
    {
        [TestMethod]
        public void GetFiles_FolderContainsFrom1To3JpgWithXmlFilesAnd1Txt_ReturnsFrom1To3JpgFilesList()
        {
            //arrange
            var provider = new ImageFilesDataProvider()
            {
                Folder = Path.Combine(Directory.GetCurrentDirectory(), @"Files")
            };
            var expectedResult = new List<string>
            {
                Path.Combine(provider.Folder, "1.jpg"),
                Path.Combine(provider.Folder, "2.jpg"),
                Path.Combine(provider.Folder, "3.jpg"),
            };

            //act
            var result = provider.GetFiles();

            for(int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }
        }

        [TestMethod]
        public void GetFiles_FolderNotContainsImgs_ReturnsEmptyList()
        {
            //arrange
            var provider = new ImageFilesDataProvider()
            {
                Folder = Path.Combine(Directory.GetCurrentDirectory(), @"NotImages")
            };
            var expectedResultCount = 0;

            //act
            var result = provider.GetFiles();

            
            Assert.AreEqual(expectedResultCount, result.Count);
        }

        [TestMethod]
        public void File_SetPropertyFileWhichDoesntExist_ThrowsFileNotFoundException()
        {
            //arrange
            var provider = new ImageFilesDataProvider()
            {
                Folder = Path.Combine(Directory.GetCurrentDirectory(), @"Images")
            };

            //act
            var action = new Action(() =>
            {
                provider.File = "abc.jpg";
            });


            Assert.ThrowsException<FileNotFoundException>(action);
        }

        [TestMethod]
        public void File_SetPropertyExistingFileInSameFolder_SetFileAs1Jpg()
        {
            //arrange
            var provider = new ImageFilesDataProvider()
            {
                Folder = Path.Combine(Directory.GetCurrentDirectory(), @"Files")
            };
            var expected = "1.jpg";

            //act
            provider.File = Path.Combine(provider.Folder, "1.jpg");


            Assert.AreEqual(Path.Combine(provider.Folder, expected), provider.File);
        }

        [TestMethod]
        public void File_SetPropertyExistingFileInAnotherFolder_SetFileAs1JpgAndFolderAsFiles()
        {
            //arrange
            var provider = new ImageFilesDataProvider()
            {
                Folder = Path.Combine(Directory.GetCurrentDirectory(), @"NotImages")
            };
            var targetFolderName = @"Files";

            var expectedFileName = "1.jpg";
            var expectedFolder = Path.Combine(Directory.GetCurrentDirectory(), @"Files");

            //act
            provider.File = Path.Combine(Directory.GetCurrentDirectory(), targetFolderName, "1.jpg");


            Assert.AreEqual(Path.Combine(expectedFolder, expectedFileName), provider.File);
            Assert.AreEqual(expectedFolder, provider.Folder);
        }
    }
}
