using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using TransIT.DAL.Models.DTOs;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    class FileSystemStorage : IFileStorageLogger
    {
        public void Create(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public void Delete(string FilePath)
        {
            throw new NotImplementedException();
        }

        public IFormFile Download(string FilePath)
        {
            throw new NotImplementedException();
        }
    }
}
