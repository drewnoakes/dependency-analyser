using System;
using System.IO;

namespace Drew.DependencyAnalyser
{
    public class TemporaryFileManager
    {
        private readonly Random _random = new Random();

        public string CreateTemporaryFile()
        {
            var tempFilename = GenerateTemporaryFilename();
            while (File.Exists(tempFilename))
                tempFilename = GenerateTemporaryFilename();
            File.Create(tempFilename).Close();
            return tempFilename;
        }

        private string GenerateTemporaryFilename()
        {
            // TODO loop until file not found
            return Path.Combine(Path.GetTempPath(), String.Format("DependencyAnaylser.{0:X}.tmp", _random.Next(int.MaxValue >> 4)));
        }

        public static void DeleteAllTemporaryFiles()
        {
            var filenames = GetExistingTemporaryFilenames();
            foreach (var filename in filenames)
            {
                try
                {
                    File.Delete(filename);
                }
                catch
                {
                    // file is probably locked
                }
            }
        }

        public static string[] GetExistingTemporaryFilenames()
        {
            var fileInfoArray = new DirectoryInfo(Path.GetTempPath()).GetFiles("DependencyAnaylser.*.tmp");
            var filenames = new string[fileInfoArray.Length];
            for (var index = 0; index < fileInfoArray.Length; index++)
                filenames[index] = fileInfoArray[index].FullName;
            return filenames;
        }
    }
}