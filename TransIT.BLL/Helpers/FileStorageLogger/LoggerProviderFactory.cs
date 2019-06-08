using System;
using System.Collections.Generic;
using System.Text;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    public static class LoggerProviderFactory
    {
        public static IFileStorageLogger GetFileStorageLogger()
        {
            return new AzureFileStorage();
        }
    }
}
