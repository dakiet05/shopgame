using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xuonggame.Models;

namespace xuonggame.Areas.ADMIN.Controllers
{
        [RouteArea("admin")]
        [Route("admin")]
        [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}