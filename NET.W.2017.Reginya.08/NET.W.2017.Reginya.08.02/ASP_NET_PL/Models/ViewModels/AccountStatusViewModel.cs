using System.ComponentModel.DataAnnotations;

namespace ASP_NET_PL.Models.ViewModels
{
    public class AccountStatusViewModel
    {
        [Display(Name = "Account type")]
        public string Type { get; set; }

        [Display(Name = "Account number")]
        public string AccountId { get; set; }

        [Display(Name = "Owner's first name")]
        public string OwnerFirstName { get; set; }

        [Display(Name = "Owner's second name")]
        public string OwnerSecondName { get; set; }

        [Display(Name = "Owner's email address")]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; }

        [Display(Name = "Current account balance")]
        [DataType(DataType.Currency)]
        public string Balance { get; set; }

        [Display(Name = "Bonus points")]
        public string Bonus { get; set; }        
    }
}