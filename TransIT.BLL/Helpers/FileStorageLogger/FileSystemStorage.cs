using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    class FileSystemStorage : IFileStorageLogger
    {
        public string Create(IFormFile file)
        {
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "//source//" + "TransportITDocuments";
            //  var filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + "TransportITDocuments";
            System.IO.Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, DateTime.Now.ToString("MM/dd/yyyy/HH/mm/ss") + file.FileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                 file.CopyTo(fileStream);
            }
            return filePath;
        }

        public void Delete(string FilePath)
        {
            var fileInfo = new System.IO.FileInfo(FilePath);
            fileInfo.Delete();
        }

        public byte[] Download(string FilePath)
        {
            return System.IO.File.ReadAllBytes(FilePath);
        }
    }
}
