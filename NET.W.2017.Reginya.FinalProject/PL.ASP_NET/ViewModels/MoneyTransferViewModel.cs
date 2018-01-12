using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET.ViewModels
{
    public class MoneyTransferViewModel
    {
        [Display(Name = "Source account number")]        
        public string FromAccountNumber { get; set; }

        [Display(Name = "Destination account number")]
        [RegularExpression(@"[0-9]{5}[a-z]{4}[0-9]{11}", ErrorMessage = "Invalid email")]
        public string ToAccountNumber { get; set; }

        [Display(Name = "Destination email address")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        [DataType(DataType.EmailAddress)]
        public string ToEmail { get; set; }

        [Display(Name = "Transfer sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "Transfer sum must be at least 0 and not more than 50000")]
        public decimal TransferSum { get; set; }
    }
}