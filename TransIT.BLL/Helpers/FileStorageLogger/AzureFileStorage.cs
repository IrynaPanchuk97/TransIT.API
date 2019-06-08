using System;
using Microsoft.AspNetCore.Http;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using Microsoft.Azure;
using System.IO;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    class AzureFileStorage : IFileStorageLogger
    {
        private readonly IConfiguration _configuration;
        private readonly string StorageAccountName = "transitdocuments";
        private readonly string StorageAccountkey = "pqu5uvteuvJqrcC2qLkxZH71MyV1bOPbzJKGOGTMGY73qlVxTTPNFDpkNLNH/pu6E9n7N7uJTdCya8u/TwtmEw==";
        private readonly string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=transitdocuments;AccountKey=pqu5uvteuvJqrcC2qLkxZH71MyV1bOPbzJKGOGTMGY73qlVxTTPNFDpkNLNH/pu6E9n7N7uJTdCya8u/TwtmEw==;EndpointSuffix=core.windows.net";

        public string Create(IFormFile file)
        {
            var task = CreateAsync(file);
            return task.Result.ToString();

        }

        private async Task<Uri> CreateAsync(IFormFile file)
        {
            CloudStorageAccount storageAccount = null;
            if (CloudStorageAccount.TryParse(StorageConnectionString, out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transdocuments");
                await container.CreateIfNotExistsAsync();
                if (await container.CreateIfNotExistsAsync())
                {

                    await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                }
                CloudBlockBlob blob = container.GetBlockBlobReference(DateTime.Now.ToString("MM/dd/yyyy/HH/mm/ss") + file.FileName);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    await blob.UploadFromStreamAsync(memoryStream);
                    return blob.Uri;
                }
            }
            return null;
        }

        public void Delete(string FilePath)
        {
            CloudStorageAccount storageAccount = null;
            if (CloudStorageAccount.TryParse(_configuration.GetConnectionString("transitdocuments"), out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transITDocumentsUpload");
                container.GetBlockBlobReference(FilePath).DeleteIfExistsAsync();
            }
        }

        public byte[] Download(string FilePath)
        {
            CloudStorageAccount storageAccount = null;
            if (CloudStorageAccount.TryParse(_configuration.GetConnectionString("transitdocuments"), out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transITDocumentsUpload");
                CloudBlobDirectory dira = container.GetDirectoryReference("FolderName");

                //Gets List of Blobs

               // var list = dira.;

              //  List<string> blobNames = list.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();


            }
            return null;
        }


        /////
        //public string DeleteImage(string Name)
        //{
        //    Uri uri = new Uri(Name);
        //    string filename = System.IO.Path.GetFileName(uri.LocalPath);

        //    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
        //    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);

        //    blob.Delete();

        //    return "File Deleted";
        //}

        //==================
    }
}