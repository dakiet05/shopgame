using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xuonggame.Models;

namespace xuonggame.Controllers
{
    public class GameDetailController : Controller
    {
        // GET: GameDetail
        public ActionResult Index(int id = 1)
        {
            XUONGGAME_DBEntities1 dBContext = new XUONGGAME_DBEntities1();
            Product_Category product = dBContext.Product_Category.FirstOrDefault(x => x.ProId == id);
            return View(product);
        }
    }
}