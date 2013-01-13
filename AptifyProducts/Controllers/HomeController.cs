using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptifyWebApi.Controllers {
    public class HomeController : Controller {

        private ISession session;

        public HomeController(ISession session) {
            this.session = session;
        }
        public ActionResult Index() {
            return View();
        }
    }
}
