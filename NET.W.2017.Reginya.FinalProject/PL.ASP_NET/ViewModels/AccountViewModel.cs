using System.ComponentModel.DataAnnotations;
using BLL.Interface.ServiceInterface;

namespace PL.ASP_NET.ViewModels
{
    public class AccountViewModel
    {
        [Display(Name = "Account number")]        
        public string AccountNumber { get; set; }

        [Display(Name = "Account type")]        
        public AccountType Type { get; set; }
       
        [Display(Name = "Initial balance")]        
        [DataType(DataType.Currency)]        
        public decimal Balance { get; set; }

        [Display(Name = "Initial bonus")]        
        [DataType(DataType.Currency)]        
        public int Bonus { get; set; }
    }
}