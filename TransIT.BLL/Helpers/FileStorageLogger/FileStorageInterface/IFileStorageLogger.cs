using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface
{
    interface IFileStorageLogger:IFileCreate,IFileDelete,IFileDownload
    {
    }
}
