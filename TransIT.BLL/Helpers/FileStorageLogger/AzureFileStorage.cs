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
        public void Delete(string FilePath)
        {
            var task = DeleteAsync(FilePath);
        }
        public byte[] Download(string FilePath)
        {
            var task = DownloadAsync(FilePath);
            return task.Result;
        }
        private async Task<Uri> CreateAsync(IFormFile file)
        {
            CloudStorageAccount storageAccount = null;
            if (CloudStorageAccount.TryParse(StorageConnectionString, out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transitdocuments");
                await container.CreateIfNotExistsAsync();
                if (await container.CreateIfNotExistsAsync())
                {

                    await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                }
                CloudBlockBlob blob = container.GetBlockBlobReference(DateTime.Now.ToString("MM/dd/yyyy/HH/mm/ss") + file.FileName);
                blob.Properties.ContentType = file.ContentType;
                using (var memoryStream = new MemoryStream())
                {
                    file.OpenReadStream();
                    await file.CopyToAsync(memoryStream);
                    await blob.UploadFromStreamAsync(memoryStream);
                    return blob.Uri;
                }
            }
            return null;
        }

        public async Task DeleteAsync(string path)
        {
            CloudStorageAccount storageAccount = null;

            if (CloudStorageAccount.TryParse(StorageConnectionString, out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transitdocuments");
                CloudBlockBlob _blockBlob = container.GetBlockBlobReference(Path.GetFileName(path));
                //delete blob from container    
                await _blockBlob.DeleteAsync();

            }

        }

        public async Task<byte[]> DownloadAsync(string path)
        {
            CloudStorageAccount storageAccount = null;
            byte[] result;
            if (!CloudStorageAccount.TryParse(StorageConnectionString, out storageAccount)) return null;         
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transitdocuments");
                CloudBlockBlob _blockBlob = container.GetBlockBlobReference(Path.GetFileName(path));
                using (var mStream = new MemoryStream())
                {
                   await _blockBlob.DownloadToStreamAsync(mStream);
                   result = mStream.ToArray();
                }
            return result;
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