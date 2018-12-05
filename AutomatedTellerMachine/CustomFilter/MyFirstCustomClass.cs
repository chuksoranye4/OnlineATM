using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace AutomatedTellerMachine.CustomFilter
{
    public class MyFirstCustomClass : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request.UserHostAddress;
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            filterContext.Controller.ViewBag.myMessage = String.Format("controller:{0} action:{1}", controllerName, actionName);
            //Console.Write(request.ToString());
            filterContext.Controller.ViewBag.Message = request;
            base.OnActionExecuting(filterContext);
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    filterContext.Controller.ViewBag.Message = "Override from the action executing";
        //    base.OnActionExecuted(filterContext);
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //      base.OnResultExecuted(filterContext);
        //}
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Override from the action executing";
            base.OnResultExecuting(filterContext);
        }
    }
}