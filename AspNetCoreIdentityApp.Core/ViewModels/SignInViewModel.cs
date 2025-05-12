using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Core.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel() { }

        public SignInViewModel(string email, string password) 
        {
            Email = email;
            Password = password;

        }
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Required(ErrorMessage = "Email boş bırakılamaz.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre zorunludur.")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }


        [Display(Name = "Beni Hatırla ")]
        public bool RememberMe { get; set; }
    }
}
