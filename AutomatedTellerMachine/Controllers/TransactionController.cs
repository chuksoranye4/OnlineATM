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
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        // Making deposition
        [HttpPost]
        public ActionResult Deposit(Transaction transactions)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                var userAccount =  db.CheckingAccounts.Where(p => p.ApplicationUserId == userID).First();
                var newBalance = transactions.Amount + userAccount.Balance;
                userAccount.Balance = newBalance;

                var transactionTable = new Transaction();
                transactionTable.Amount = transactions.Amount;
                transactionTable.CheckingAccountId = userAccount.Id;
                db.Transactions.Add(transactionTable);
                db.SaveChanges();
                return RedirectToAction("Details", "CheckingAccount");
            }
            return View();
        }


        //Transaction history[Print] by admins and user
        [Authorize]
        public ActionResult ListHistory(TransactionHistoryViewModel tt)
        {
            if (User.IsInRole("Admin"))
            {
                  var transactionList = db.Transactions.Include(p => p.CheckingAccount).Take(10).
                    Select(r => new TransactionHistoryViewModel
                    {
                        AccountNumber = r.CheckingAccount.AccountNumber,
                        FirstName = r.CheckingAccount.FirstName,
                        LastName = r.CheckingAccount.LastName,
                        Amount = r.Amount,
                        Id = r.Id
                    });
                return View(transactionList.ToList());
            }
            else
            {
                var userID = User.Identity.GetUserId();
                var transactionList = db.Transactions.Include(c => c.CheckingAccount).
                    Where(t => t.CheckingAccount.ApplicationUserId == userID).Take(10).
                    Select(r=> new TransactionHistoryViewModel {
                        AccountNumber = r.CheckingAccount.AccountNumber,
                        FirstName = r.CheckingAccount.FirstName,
                        LastName = r.CheckingAccount.LastName,
                        Amount = r.Amount,
                        Id = r.Id
                    }).ToList();
                return View(transactionList);
            }            
        }
    }
}