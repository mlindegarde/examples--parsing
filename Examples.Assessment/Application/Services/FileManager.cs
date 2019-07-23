using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Assessment.Application.Services
{
    public class FileManager : IFileManager
    {
        #region Methods
        public List<FileInfo> GetUploadedFiles(string uploadsPath)
        {
            return 
                new DirectoryInfo(uploadsPath)
                    .EnumerateFiles("*.txt")
                    .ToList();
        }

        public async Task<List<string>> ReadFileAsync(FileInfo fileInfo)
        {
            List<string> result = new List<string>();

            using(StreamReader reader = File.OpenText(fileInfo.FullName))
            {
                string line;

                while((line = await reader.ReadLineAsync()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
        #endregion
    }
}
