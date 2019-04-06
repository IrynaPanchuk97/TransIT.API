using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransIT.BLL.Services
{
    public interface IAuthentificationService
    {
        Task<string> Authentificate(string lojin, string password);
    }
}
