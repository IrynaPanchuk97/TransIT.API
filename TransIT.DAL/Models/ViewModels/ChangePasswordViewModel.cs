using System.ComponentModel.DataAnnotations;

namespace TransIT.DAL.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
