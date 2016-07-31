using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MichellesWebsite.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string Role { get; set; }
    }
    public class Address
    {
        [Key]
        public int id { get; set; }
        public string userId { get; set; }
        [Required]
        [Display(Name = "Address1", ResourceType = typeof(ViewRes.SharedStrings))]
        public string firstLine { get; set; }
        [Display(Name = "Address2", ResourceType = typeof(ViewRes.SharedStrings))]
        public string secondLine { get; set; }
        [Required]
        [Display(Name = "Postcode", ResourceType = typeof(ViewRes.SharedStrings))]
        public string postcode { get; set; }
        [Display(Name = "City", ResourceType = typeof(ViewRes.SharedStrings))]
        [Required]
        public string city { get; set; }
        [Required]
        [Display(Name = "Country", ResourceType = typeof(ViewRes.SharedStrings))]
        public Country country { get; set; }
        
    }
        public enum Country
        {
            [Display(Name = "UK", ResourceType = typeof(ViewRes.SharedStrings))]
            UK = 1,
            [Display(Name = "China", ResourceType = typeof(ViewRes.SharedStrings))]
            ZH = 2
        }
    public class CreateUserViewModel
    {
        [Required]
        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.SharedStrings), MinimumLength = 6,
                      ErrorMessageResourceName = "PasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ViewRes.SharedStrings))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "FullName", ResourceType = typeof(ViewRes.SharedStrings))]
        public string FullName { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
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

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.SharedStrings), MinimumLength = 6,
                      ErrorMessageResourceName = "PasswordError")]
        [Display(Name = "Password", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(ViewRes.SharedStrings))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.SharedStrings), MinimumLength = 6,
                      ErrorMessageResourceName = "PasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ViewRes.SharedStrings))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "FullName", ResourceType = typeof(ViewRes.SharedStrings))]
        public string FullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "TelephoneNumber", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Number { get; set; }

        [Required]
        public Address address { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.SharedStrings), MinimumLength = 6,
                      ErrorMessageResourceName = "PasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ViewRes.SharedStrings))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Email { get; set; }
    }
}
