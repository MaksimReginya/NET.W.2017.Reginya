using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET.ViewModels
{
    public class LoginViewModel
    {        
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]        
        public string Email { get; set; }
       
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "The password value cannot exceed 20 characters")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }       
    }
}