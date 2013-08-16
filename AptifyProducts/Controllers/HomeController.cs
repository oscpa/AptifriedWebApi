#region using

using System.Web.Mvc;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class HomeController : Controller
    {
        private ISession session;

        public HomeController(ISession session)
        {
            this.session = session;
        }


        public ActionResult Index()
        {
            //TODO: Testing page for search
            return View();
        }
    }
}