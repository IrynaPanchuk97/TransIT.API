using System;
using Microsoft.AspNetCore.Http;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    class AzureFileStorage : IFileStorageLogger
    {
        private readonly IConfiguration _configuration;

        public string Create(IFormFile file)
        {
          Task<Uri> uri = Task.Run<Uri>(async () => await CreateAsync(file));
            if (uri.Result != null)
            {
                uri.ToString();
            }
            return "";
        }

        private async Task<Uri> CreateAsync(IFormFile file)
        {
            CloudStorageAccount storageAccount = null;
            if (CloudStorageAccount.TryParse(_configuration.GetConnectionString("StorageAccount"), out storageAccount))
            {
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("transITDocumentsUpload");
                await container.CreateIfNotExistsAsync();

                var blob = await container.GetBlobReferenceFromServerAsync(file.FileName);
                await blob.UploadFromStreamAsync(file.OpenReadStream());

                return blob.Uri;
            }
            return null;
        }

        public void Delete(string FilePath)
        {
            throw new NotImplementedException();
        }

        public byte[] Download(string FilePath)
        {
            throw new NotImplementedException();
        }
    }
}
