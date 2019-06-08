using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface
{
    interface IFileDownload
    {
        IFormFile Download(string FilePath);
    }
}
