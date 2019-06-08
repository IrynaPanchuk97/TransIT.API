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
         Task<IActionResult> Create([FromForm] DocumentDTO document);
    }
}
