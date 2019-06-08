using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using TransIT.DAL.Models.DTOs;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    class AzureFileStorage : IFileStorageLogger
    {
        public Task<IActionResult> Create([FromForm] DocumentDTO document)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DownloadFile(int id)
        {
            throw new NotImplementedException();
        }
    }
}
