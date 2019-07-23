using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Assessment.Application.Services
{
    public interface IFileManager
    {
        #region Methods
        List<FileInfo> GetUploadedFiles(string uploadsPath);
        Task<List<string>> ReadFileAsync(FileInfo fileInfo);
        #endregion
    }
}
