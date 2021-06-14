using System.IO;

using Xunit;

namespace DependencyAnalyser.Tests
{
    public sealed class TemporaryFileManagerTest
    {
        [Fact]
        public void CreateTemporaryFile_Format()
        {
            var filename = TemporaryFileManager.CreateTemporaryFile();
            Assert.StartsWith("DependencyAnaylser.", Path.GetFileName(filename));
            Assert.EndsWith(".tmp", Path.GetFileName(filename));
            Assert.Equal(Path.GetTempPath(), Path.GetDirectoryName(filename) + "\\");
        }

        [Fact]
        public void CreateTemporaryFile_CreatesZeroByteFile()
        {
            var filename = TemporaryFileManager.CreateTemporaryFile();
            Assert.True(File.Exists(filename));
            Assert.Equal(0L, new FileInfo(filename).Length);
        }

        [Fact]
        public void GetExistingTemporaryFilenames()
        {
            TemporaryFileManager.DeleteAllTemporaryFiles();
            var filename1 = TemporaryFileManager.CreateTemporaryFile();
            var filename2 = TemporaryFileManager.CreateTemporaryFile();
            var filenames = TemporaryFileManager.GetExistingTemporaryFilenames();
            Assert.Equal(2, filenames.Length);
            Assert.True(filename1!=filename2);
            Assert.True(filename1==filenames[0] || filename1==filenames[1]);
            Assert.True(filename2==filenames[0] || filename2==filenames[1]);
        }

        [Fact]
        public void DeleteAllTemporaryFiles()
        {
            TemporaryFileManager.DeleteAllTemporaryFiles();
            TemporaryFileManager.CreateTemporaryFile();
            TemporaryFileManager.CreateTemporaryFile();
            Assert.Equal(2, TemporaryFileManager.GetExistingTemporaryFilenames().Length);
            TemporaryFileManager.DeleteAllTemporaryFiles();
            Assert.Empty(TemporaryFileManager.GetExistingTemporaryFilenames());
        }
    }
}
