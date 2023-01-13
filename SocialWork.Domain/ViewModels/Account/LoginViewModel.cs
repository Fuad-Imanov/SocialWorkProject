using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialWork.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kod")]
        public string? Password { get; set; }

        [Display(Name = "Yadda saxla?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
