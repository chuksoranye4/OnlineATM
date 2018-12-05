using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class TransactionHistoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Account #")]
        public string AccountNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
    }
}