namespace CarDealerApp.Filters
{
    using System.IO;
    using System.Web.Mvc;
    using System;
    using CarDealer.Models;
    using Security;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string Date = DateTime.Now.ToString();
            string Ip = filterContext.HttpContext.Request.UserHostAddress;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            string currentUser = "Anonymous";
            User user = SignInManager.GetAuthenticatedUser(filterContext.HttpContext.Session.SessionID);
            if (user != null)
            {
                currentUser = user.Username;
            }
            string logMsg = $"{Date} - {Ip} - {currentUser} - {controllerName}.{actionName}\r\n";

            var exception = filterContext.Exception;
            if (exception != null)
            {
                logMsg = $"[!] {Date} - {Ip} - {currentUser} - {controllerName}.{actionName} - {exception.GetType().Name} - {exception.Message}\r\n";
            }

            File.AppendAllText("D:\\log.txt", logMsg);

            base.OnActionExecuted(filterContext);
        }
    }
}