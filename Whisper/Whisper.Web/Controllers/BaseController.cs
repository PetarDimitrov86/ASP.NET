namespace Whisper.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Contracts;

    public class BaseController : Controller
    {
        private ITwitterData context;

        protected BaseController(ITwitterData data)
        {
            this.context = data;
        }

        public ITwitterData Context { get; }
    }
}