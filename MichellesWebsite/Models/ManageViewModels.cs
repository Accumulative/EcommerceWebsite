﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MichellesWebsite.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        public string email { get; set; }
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.SharedStrings), MinimumLength = 6,
                      ErrorMessageResourceName = "PasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ViewRes.SharedStrings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ViewRes.SharedStrings))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(ViewRes.SharedStrings))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.SharedStrings), MinimumLength = 6,
                      ErrorMessageResourceName = "PasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ViewRes.SharedStrings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ViewRes.SharedStrings))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "TelephoneNumber", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "TelephoneNumber", ResourceType = typeof(ViewRes.SharedStrings))]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}