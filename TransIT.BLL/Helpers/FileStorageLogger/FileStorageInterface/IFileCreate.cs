using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransIT.DAL.Models.DTOs;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    interface IFileCreate
    {
         void Create(IFormFile file);
    }
}
