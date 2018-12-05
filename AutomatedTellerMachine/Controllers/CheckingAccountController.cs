using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    [Authorize]
    public class CheckingAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CheckingAccount
        public ActionResult Index()
        {
            return View();
        }

        // GET: CheckingAccount/Details
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccount = db.CheckingAccounts.Where(p => p.ApplicationUserId == userId).First();
            return View(checkingAccount);
        }

        // GET: CheckingAccount/List  all account for admin
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            return View(db.CheckingAccounts.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DetailsForAdmin(int Id)
        {
            var checkingAccount = db.CheckingAccounts.Find(Id);
            return View("Details", checkingAccount);
        }

        [Authorize]
        public ActionResult Withdrawal()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Withdrawal(WithdrawalViewModel withDrawal)
        {
            var userID = User.Identity.GetUserId();
            var enteredBalance = withDrawal.Balance;
            var user = db.CheckingAccounts.Where(p => p.ApplicationUserId == userID).First();
            var balance = user.Balance;

            if (balance - enteredBalance < 0)
            {
                ModelState.AddModelError("Amount", "You have insufficient found to execute this transaction");
            }

            if (ModelState.IsValid)
            {
                var newbalance = balance - enteredBalance;
                user.Balance = newbalance;
                var transactionTable = new Transaction();
                transactionTable.Amount = 0- enteredBalance;
                transactionTable.CheckingAccountId = user.Id;
                db.Transactions.Add(transactionTable);
                db.SaveChanges();
                return RedirectToAction("Details", "CheckingAccount"); 
            }
            return View();
        }
        public ActionResult QuickCash()
        {
            var userID = User.Identity.GetUserId();
            var enteredBalance = 100;
            var user = db.CheckingAccounts.Where(p => p.ApplicationUserId == userID).First();
            var balance = user.Balance;
            if (balance - enteredBalance < 0)
            {
                ViewBag.Message = "You have insufficient found to execute this transaction";
                return View();
            }
            var newAccountBalance = balance - enteredBalance;
            ViewBag.Message = String.Format("Your new account balance is {0:c}.", newAccountBalance.ToString());
            return View();
        }

        [Authorize]
        public ActionResult QuickCashe()
        {
            try
            {
                var userID = User.Identity.GetUserId();
                var enteredBalance = 100;
                var user = db.CheckingAccounts.Where(p => p.ApplicationUserId == userID).First();
                var balance = user.Balance;
                var newbalance = balance - enteredBalance;
                if (newbalance < 0)
                {
                    ViewBag.Message = "You have insufficient found to execute this transaction";
                    return View();
                }
                user.Balance = newbalance;
                var transactionTable = new Transaction();
                transactionTable.Amount = 0 - enteredBalance;
                transactionTable.CheckingAccountId = user.Id;
                db.Transactions.Add(transactionTable);
                db.SaveChanges();
                return RedirectToAction("Details", "CheckingAccount");
            }
            catch (Exception exp)
            {
                ViewBag.Message = exp.Message.ToString();
                return View();
            }
        }
        public ActionResult TransferFund()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult TransferFund(TransferViewModels transferFund)
        {
            var userID = User.Identity.GetUserId();
            var amount = transferFund.Amount;
            var toAccount = transferFund.AccountNumber;
            var user = db.CheckingAccounts.Where(p => p.ApplicationUserId == userID).First();
            var fromAccount = user.AccountNumber;
            var fromBalance = user.Balance;
            var fromCheckingAccountId = user.Id; //

            var checkingToUser = db.CheckingAccounts.Where(p => p.AccountNumber == toAccount).SingleOrDefault();
            if (checkingToUser == null)
            {
                ModelState.AddModelError("Account", "Sorry, the sending account does not exists");
            }
            if (fromBalance - amount < 0)
            {
                ModelState.AddModelError("Amount", "You have insufficient found to execute this transaction");
            }

            if (ModelState.IsValid)
            {
                //implement fund transfer
                var newFromBalance = fromBalance - amount;
                var newToBalance = checkingToUser.Balance + amount;
                user.Balance = newFromBalance;
                checkingToUser.Balance = newToBalance;
                //add transactions to the transaction Table
                var transactionTable = new Transaction();
                transactionTable.Amount = 0 - amount;
                transactionTable.CheckingAccountId = user.Id;
                transactionTable.Amount = amount;
                transactionTable.CheckingAccountId = checkingToUser.Id;
                db.Transactions.Add(transactionTable);
                db.SaveChanges();
                return RedirectToAction("Details", "CheckingAccount");
            }
            return View();

        }
    }
}
