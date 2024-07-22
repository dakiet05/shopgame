using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xuonggame.Models;

namespace xuonggame.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            XUONGGAME_DBEntities1 dBContext = new XUONGGAME_DBEntities1();
            List<Product_Category> ListProducts = dBContext.Product_Category.ToList();
            return View(ListProducts);
        }

    }
}