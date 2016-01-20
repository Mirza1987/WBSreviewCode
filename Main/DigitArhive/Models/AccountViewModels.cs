using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitArchive.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamtiti ovaj preglednik?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unos e-maila je obavezan.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Vaš unos nije validna e-mail adresa.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unos passworda je obavezan.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Zapamti?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name="Ime")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name="Prezime")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Unos e-maila je obavezan.")]
        [EmailAddress(ErrorMessage = "Vaš unos nije validna e-mail adresa.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unos passworda je obavezan.")]
        [StringLength(100, ErrorMessage = "{0} mora imati najmanje {2} znakova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi password")]
        [Compare("Password", ErrorMessage = "Password i password potvrde se ne podudaraju.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Unos e-maila je obavezan.")]
        [EmailAddress(ErrorMessage = "Vaš unos nije validna e-mail adresa.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unos passworda je obavezan.")]
        [StringLength(100, ErrorMessage = "{0} mora imati najmanje {2} znakova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi password")]
        [Compare("Password", ErrorMessage = "Password i password potvrde se ne podudaraju.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Unos passworda je obavezan.")]
        [EmailAddress(ErrorMessage = "Vaš unos nije validna e-mail adresa.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
