using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET.ViewModels
{
    public class CloseAccountViewModel
    {
        [Display(Name = "Account number")]        
        public string AccountNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}