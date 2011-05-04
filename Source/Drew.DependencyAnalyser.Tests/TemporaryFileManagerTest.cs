using System.IO;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser.Tests
{
    [TestFixture]
    public sealed class TemporaryFileManagerTest
    {
        private TemporaryFileManager _temporaryFileManager;

        [SetUp]
        public void SetUp()
        {
            _temporaryFileManager = new TemporaryFileManager();
        }

        [Test]
        public void CreateTemporaryFile_Format()
        {
            var filename = _temporaryFileManager.CreateTemporaryFile();
            Assertion.Assert(Path.GetFileName(filename).StartsWith("DependencyAnaylser."));
            Assertion.Assert(Path.GetFileName(filename).EndsWith(".tmp"));
            Assertion.AssertEquals(Path.GetTempPath(), Path.GetDirectoryName(filename) + "\\");
        }

        [Test]
        public void CreateTemporaryFile_CreatesZeroByteFile()
        {
            var filename = _temporaryFileManager.CreateTemporaryFile();
            Assertion.Assert(File.Exists(filename));
            Assertion.AssertEquals(0L, new FileInfo(filename).Length);
        }

        [Test]
        public void GetExistingTemporaryFilenames()
        {
            TemporaryFileManager.DeleteAllTemporaryFiles();
            var filename1 = _temporaryFileManager.CreateTemporaryFile();
            var filename2 = _temporaryFileManager.CreateTemporaryFile();
            var filenames = TemporaryFileManager.GetExistingTemporaryFilenames();
            Assertion.AssertEquals("number of temp files", 2, filenames.Length);
            Assertion.Assert(filename1!=filename2);
            Assertion.Assert(filename1==filenames[0] || filename1==filenames[1]);
            Assertion.Assert(filename2==filenames[0] || filename2==filenames[1]);
        }

        [Test]
        public void DeleteAllTemporaryFiles()
        {
            TemporaryFileManager.DeleteAllTemporaryFiles();
            _temporaryFileManager.CreateTemporaryFile();
            _temporaryFileManager.CreateTemporaryFile();
            Assertion.AssertEquals(2, TemporaryFileManager.GetExistingTemporaryFilenames().Length);
            TemporaryFileManager.DeleteAllTemporaryFiles();
            Assertion.AssertEquals(0, TemporaryFileManager.GetExistingTemporaryFilenames().Length);
        }
    }
}
