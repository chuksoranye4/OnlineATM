using AutomatedTellerMachine.CustomFilter; // adding of the custom filter class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine.Controllers
{
    //[Authorize]
    //[MyFirstCustomClass]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.Where(p => p.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            return View();
           // return PartialView();
        }

        public ActionResult About()
        {
            return View();
        }

       // [AllowAnonymous]
       //[OutputCache(Duration =1200,VaryByParam ="id")]
       [MyFirstCustomClass]
        public ActionResult Contact()
        {
            ViewBag.TheMessage = "Your can contact uschuksoranye4@yahoo.com"; //valid only when you returen the view
            //TempData[Message] = "Your contact page."; //still remain availiable after redirect
            return View();
        }
                
       // [Authorize( Roles = "", Users = "Admin,Marketing")]
        // [HandleError(View="MyError")] to return a particular error view
        [HttpPost]
        public ActionResult Contact(string message) //message = the textarea in the form
        {
            ViewBag.TheMessage = "Thanks we got your message"; //valid only when you returen the view
            //TempData[Message] = "Your contact page."; //still remain availiable after redirect
            return PartialView("_ContactThanks");
        }        
        public ActionResult Foo()
        {
            return View("About");
        }

        //GET/Serial?letterCase=lower
        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPMVCATM1";
            if (letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            return Content(serial.ToUpper());
            // return Json(new { name = "serial", value = serial }, JsonRequestBehavior.AllowGet);
           // return RedirectToAction("Index");
        }
    }
}