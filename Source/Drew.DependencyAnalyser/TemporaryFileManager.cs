using System;
using System.IO;

namespace Drew.DependencyAnalyser
{
    public static class TemporaryFileManager
    {
        private static readonly Random _random = new Random();

        public static string CreateTemporaryFile()
        {
            var tempFilename = GenerateTemporaryFilename();
            while (File.Exists(tempFilename))
                tempFilename = GenerateTemporaryFilename();
            File.Create(tempFilename).Close();
            return tempFilename;
        }

        private static string GenerateTemporaryFilename()
        {
            // TODO loop until file not found
            return Path.Combine(Path.GetTempPath(), $"DependencyAnaylser.{_random.Next(int.MaxValue >> 4):X}.tmp");
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