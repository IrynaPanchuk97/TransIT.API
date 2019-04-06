using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Security
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public long AccessTokenExpiration { get; set; }
    }
}
