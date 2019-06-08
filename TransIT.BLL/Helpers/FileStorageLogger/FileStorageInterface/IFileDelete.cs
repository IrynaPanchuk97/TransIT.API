using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface
{
    interface IFileDelete
    {
        void Delete(string FilePath);
    }
}
