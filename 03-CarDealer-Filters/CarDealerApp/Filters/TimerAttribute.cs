namespace CarDealerApp.Filters
{
    using System.Web.Mvc;
    using System;
    using System.IO;

    public class TimerAttribute : ActionFilterAttribute
    {
        private DateTime time;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            time = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TimeSpan span = DateTime.Now - time;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            string timerMsg = $"{DateTime.Now} - {controllerName}.{actionName} - {span}\r\n";

            File.AppendAllText("D:\\action-times.txt", timerMsg);

            base.OnActionExecuted(filterContext);
        }
    }
}