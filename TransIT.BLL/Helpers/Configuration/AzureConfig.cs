using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Helpers.Configuration
{
    static class AzureConfig
    {
        public static readonly string StorageAccountName = "transitdocuments";
        public static readonly string StorageAccountkey = "pqu5uvteuvJqrcC2qLkxZH71MyV1bOPbzJKGOGTMGY73qlVxTTPNFDpkNLNH/pu6E9n7N7uJTdCya8u/TwtmEw==";
        public static readonly string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=transitdocuments;AccountKey=pqu5uvteuvJqrcC2qLkxZH71MyV1bOPbzJKGOGTMGY73qlVxTTPNFDpkNLNH/pu6E9n7N7uJTdCya8u/TwtmEw==;EndpointSuffix=core.windows.net";
    }
}
