using System.ComponentModel.DataAnnotations;

namespace ASP_NET_PL.Models.ViewModels
{
    public class TransactionViewModel
    {
        [Display(Name = "Source account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string FromAccountNumber { get; set; }

        [Display(Name = "Destination account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string ToAccountNumber { get; set; }

        [Display(Name = "Transaction sum")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The sum must be at least 0 and not more than 500,000")]
        public decimal Sum { get; set; }
    }
}