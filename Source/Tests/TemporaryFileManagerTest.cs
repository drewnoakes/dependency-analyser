using System.IO;

using NUnit.Framework;

namespace Drew.DependencyPlotter.Tests
{
	/// <summary>
	/// Summary description for TemporaryFileManagerTest.
	/// </summary>
	[TestFixture]
	public class TemporaryFileManagerTest
	{
		TemporaryFileManager _temporaryFileManager;

		[SetUp]
		public void SetUp()
		{
			_temporaryFileManager = new TemporaryFileManager();
		}
		[Test]
		public void testCreateTemporaryFile_Format()
		{
			string filename = _temporaryFileManager.CreateTemporaryFile();
			Assertion.Assert(Path.GetFileName(filename).StartsWith("DependancyAnaylser."));
			Assertion.Assert(Path.GetFileName(filename).EndsWith(".tmp"));
			Assertion.AssertEquals(Path.GetTempPath(), Path.GetDirectoryName(filename) + "\\");
		}

		[Test]
		public void testCreateTemporaryFile_CreatesZeroByteFile()
		{
			string filename = _temporaryFileManager.CreateTemporaryFile();
			Assertion.Assert(File.Exists(filename));
			Assertion.AssertEquals(0L, new FileInfo(filename).Length);
		}

		[Test]
		public void testGetExistingTemporaryFilenames()
		{
			_temporaryFileManager.DeleteAllTemporaryFiles();
			string filename1 = _temporaryFileManager.CreateTemporaryFile();
			string filename2 = _temporaryFileManager.CreateTemporaryFile();
			string[] filenames = _temporaryFileManager.GetExistingTemporaryFilenames();
			Assertion.AssertEquals("number of temp files", 2, filenames.Length);
			Assertion.Assert(filename1!=filename2);
			Assertion.Assert(filename1==filenames[0] || filename1==filenames[1]);
			Assertion.Assert(filename2==filenames[0] || filename2==filenames[1]);
		}

		[Test]
		public void testDeleteAllTemporaryFiles()
		{
			_temporaryFileManager.DeleteAllTemporaryFiles();
			_temporaryFileManager.CreateTemporaryFile();
			_temporaryFileManager.CreateTemporaryFile();
			Assertion.AssertEquals(2, _temporaryFileManager.GetExistingTemporaryFilenames().Length);
			_temporaryFileManager.DeleteAllTemporaryFiles();
			Assertion.AssertEquals(0, _temporaryFileManager.GetExistingTemporaryFilenames().Length);
		}
	}
}
