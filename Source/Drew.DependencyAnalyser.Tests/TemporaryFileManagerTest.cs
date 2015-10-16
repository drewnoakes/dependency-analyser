using System.IO;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser.Tests
{
    [TestFixture]
    public sealed class TemporaryFileManagerTest
    {
        [Test]
        public void CreateTemporaryFile_Format()
        {
            var filename = TemporaryFileManager.CreateTemporaryFile();
            Assert.IsTrue(Path.GetFileName(filename).StartsWith("DependencyAnaylser."));
            Assert.IsTrue(Path.GetFileName(filename).EndsWith(".tmp"));
            Assert.AreEqual(Path.GetTempPath(), Path.GetDirectoryName(filename) + "\\");
        }

        [Test]
        public void CreateTemporaryFile_CreatesZeroByteFile()
        {
            var filename = TemporaryFileManager.CreateTemporaryFile();
            Assert.IsTrue(File.Exists(filename));
            Assert.AreEqual(0L, new FileInfo(filename).Length);
        }

        [Test]
        public void GetExistingTemporaryFilenames()
        {
            TemporaryFileManager.DeleteAllTemporaryFiles();
            var filename1 = TemporaryFileManager.CreateTemporaryFile();
            var filename2 = TemporaryFileManager.CreateTemporaryFile();
            var filenames = TemporaryFileManager.GetExistingTemporaryFilenames();
            Assert.AreEqual(2, filenames.Length, "number of temp files");
            Assert.IsTrue(filename1!=filename2);
            Assert.IsTrue(filename1==filenames[0] || filename1==filenames[1]);
            Assert.IsTrue(filename2==filenames[0] || filename2==filenames[1]);
        }

        [Test]
        public void DeleteAllTemporaryFiles()
        {
            TemporaryFileManager.DeleteAllTemporaryFiles();
            TemporaryFileManager.CreateTemporaryFile();
            TemporaryFileManager.CreateTemporaryFile();
            Assert.AreEqual(2, TemporaryFileManager.GetExistingTemporaryFilenames().Length);
            TemporaryFileManager.DeleteAllTemporaryFiles();
            Assert.AreEqual(0, TemporaryFileManager.GetExistingTemporaryFilenames().Length);
        }
    }
}
