using System.ComponentModel.DataAnnotations;

namespace ASP_NET_PL.Models.ViewModels
{
    public class AccountOperationDataModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The initial sum must be at least 0 and not more than 500,000")]
        public decimal Sum { get; set; }
    }
}