using System.ComponentModel.DataAnnotations;
using BLL.Interface.ServiceInterface;

namespace PL.ASP_NET.ViewModels
{
    public class AccountViewModel
    {
        [Display(Name = "Account number")]
        //[Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }

        [Display(Name = "Account type")]
        //[Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType Type { get; set; }
       
        [Display(Name = "Initial balance")]
        //[Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        //[Range(typeof(decimal), "0", "50000", ErrorMessage = "The initial balance must be at least 0 and not more than 5000")]
        public decimal Balance { get; set; }

        [Display(Name = "Initial bonus")]
        //[Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        //[Range(typeof(int), "0", "500", ErrorMessage = "The initial bonus must be at least 0 and not more than 500")]
        public int Bonus { get; set; }
    }
}