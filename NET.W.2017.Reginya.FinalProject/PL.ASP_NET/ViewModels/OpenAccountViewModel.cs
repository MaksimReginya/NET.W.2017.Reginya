using System.ComponentModel.DataAnnotations;
using BLL.Interface.ServiceInterface;

namespace PL.ASP_NET.ViewModels
{
    public class OpenAccountViewModel
    {        
        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType Type { get; set; }

        [Display(Name = "Initial balance")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "The initial balance must be at least 0 and not more than 50000")]
        public decimal Balance { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}