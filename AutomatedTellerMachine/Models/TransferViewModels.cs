using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class TransferViewModels
    {
        [Required]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "The account # must be 10 digit")]
        [Display(Name = "To Account #")]
        public string AccountNumber { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Amount #")]
        public decimal Amount { get; set; }
       
    }
}