using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class WithdrawalViewModel
    {
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Amount cannot be empty and must be a digit")]
        [Display(Name = "Amount")]
        public decimal Balance { get; set; }
    }
}