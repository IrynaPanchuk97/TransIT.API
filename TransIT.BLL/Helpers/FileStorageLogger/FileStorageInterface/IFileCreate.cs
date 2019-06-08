using Microsoft.AspNetCore.Http;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    public interface IFileCreate
    {
         string Create(IFormFile file);
    }
}
