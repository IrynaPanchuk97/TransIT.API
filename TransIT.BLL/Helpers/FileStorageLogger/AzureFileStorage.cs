using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using TransIT.DAL.Models.DTOs;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    class AzureFileStorage : IFileStorageLogger
    {
        public string Create(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public void Delete(string FilePath)
        {
            throw new NotImplementedException();
        }

        public byte[] Download(string FilePath)
        {
            throw new NotImplementedException();
        }
    }
}
