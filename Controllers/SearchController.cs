using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using xuonggame.Models;
using System.Web.Mvc;

namespace xuonggame.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search(string Search = "")
        {
            var dBContext = new XUONGGAME_DBEntities1();
            var SearchItem = dBContext.Product_Category.Where(p => p.ProName.Contains(Search) || p.CatName.Contains(Search)).ToList();
            return View(SearchItem);
        }
    }
}