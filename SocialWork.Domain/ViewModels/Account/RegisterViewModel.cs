using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialWork.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Kod maksimum 100 minimum 6 simvoldan ibarət ola bilər", MinimumLength = 6)]
        [Display(Name = "Kod")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Kodlar uyğun deyil")]
        [DataType(DataType.Password)]
        [Display(Name = "Kodu təsdiqləyin")]
        public string? PasswordConfirm { get; set; }
    }
}
