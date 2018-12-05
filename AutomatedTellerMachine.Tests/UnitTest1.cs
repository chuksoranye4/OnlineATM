using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomatedTellerMachine.Controllers;

namespace AutomatedTellerMachine.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FooActionResultReturnsAboutView()
        {
            var homeController = new HomeController();
            var result = homeController.Foo() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void ContactForSayThanks()
        {
            var homeController = new HomeController();
            var result = homeController.Contact("Unit testing") as ViewResult;
            Assert.AreEqual("Thanks we got your message", result.ViewBag.TheMessage);
        }

        //public void SerialToReturnValue()
        //{
        //    var homeController = new HomeController();
        //    var result = homeController.Serial("lower") as ViewResult;
        //    Assert.IsNotNull(result.View);
        //}

        //public void ContactForSayThanks()
        //{
        //    var homeController = new HomeController();
        //    var result = homeController.Contact("Unit testing") as ViewResult;
        //    Assert.IsNotNull( result.ViewBag.TheMessage);
        //}
    }
}
