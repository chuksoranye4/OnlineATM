using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        //[RegularExpression(@"\d{1,1000}", ErrorMessage = "The amount # must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public int CheckingAccountId { get; set; }
        public virtual CheckingAccount CheckingAccount { get; set; }
    }
}