using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Core.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mevcut Şifre zorunludur.")]
        [Display(Name = "Mevcut Şifre :")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string PasswordOld { get; set; } = null!;
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [Display(Name = "Yeni Şifre :")]
        public string PasswordNew { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre Tekrar zorunludur.")]
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifreler uyuşmuyor.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [Display(Name = "Yeni Şifre Tekrar:")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
