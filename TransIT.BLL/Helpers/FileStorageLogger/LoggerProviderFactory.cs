using System;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    public static class LoggerProviderFactory
    {
        public static IFileStorageLogger GetFileStorageLogger() => new AzureFileStorage();

    }
}
