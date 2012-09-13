using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using PMI.Resources.Model;

namespace PMI.Models
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(AccountModelResources))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceName = "NewPasswordError", ErrorMessageResourceType = typeof(AccountModelResources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(AccountModelResources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(AccountModelResources))]
        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(AccountModelResources))]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "UserName", ResourceType = typeof(AccountModelResources))]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Password", ResourceType = typeof(AccountModelResources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(AccountModelResources))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "UserName", ResourceType = typeof(AccountModelResources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(AccountModelResources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceName = "NewPasswordError", ErrorMessageResourceType = typeof(AccountModelResources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(AccountModelResources))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalModelResources), ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(AccountModelResources))]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(AccountModelResources))]
        public string ConfirmPassword { get; set; }
    }
}
