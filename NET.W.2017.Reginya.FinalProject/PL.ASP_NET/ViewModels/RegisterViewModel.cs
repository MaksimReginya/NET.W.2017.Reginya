using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET.ViewModels
{
    public class RegisterViewModel
    {      
        [Display(Name = "First name")]
        [StringLength(20,  ErrorMessage = "The first name value cannot exceed 20 characters")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(20, ErrorMessage = "The last name value cannot exceed 20 characters")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
                        
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "The password value cannot exceed 20 characters")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}