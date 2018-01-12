using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET.ViewModels
{
    public class DepositWithdrawViewModel
    {
        [Display(Name = "Account number")]        
        public string AccountNumber { get; set; }

        [Display(Name = "Operation sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "Sum of operation must be at least 0 and not more than 50000")]
        public decimal OperationSum { get; set; }
    }
}