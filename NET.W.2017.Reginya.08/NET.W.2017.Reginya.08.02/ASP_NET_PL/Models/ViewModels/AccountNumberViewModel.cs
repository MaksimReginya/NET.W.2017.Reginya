using System.ComponentModel.DataAnnotations;

namespace ASP_NET_PL.Models.ViewModels
{
    public class AccountNumberViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }
    }
}