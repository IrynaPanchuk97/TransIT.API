using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransIT.API.ViewModels
{
    public class LoginModel
    {
        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string Login { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
