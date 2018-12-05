using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class CheckingAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName ="varchar")]
        [RegularExpression(@"\d{6,10}",ErrorMessage ="The account # must be between 6 to 10 digit")]
        [Display(Name = "Account #")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage ="First name cannot be empty")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Name {
            get
            {
                return string.Format("{0} {1}", this.LastName, this.FirstName);
            }
        }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}