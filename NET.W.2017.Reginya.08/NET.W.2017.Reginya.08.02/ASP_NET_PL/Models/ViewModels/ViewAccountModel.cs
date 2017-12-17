using System.ComponentModel.DataAnnotations;
using BLL.Interface.ServiceInterface;

namespace ASP_NET_PL.Models.ViewModels
{
    public class ViewAccountModel
    {
        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType Type { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string OwnerFirstName { get; set; }

        [Display(Name = "Second name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string OwnerSecondName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; }

        [Display(Name = "Initial sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "The initial sum must be at least 0 and not more than 50,000")]
        public decimal Sum { get; set; }

        [Display(Name = "Initial bonus")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(int), "0", "500", ErrorMessage = "The initial bonus must be at least 0 and not more than 500")]
        public int Bonus { get; set; }

    }
}