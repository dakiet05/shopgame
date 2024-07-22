using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xuonggame.Models;

namespace xuonggame.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id = 1)
        {
            XUONGGAME_DBEntities1 dBContext = new XUONGGAME_DBEntities1();
            Blog blogs = dBContext.Blogs.FirstOrDefault(x => x.blog_id == id);
            return View(blogs);
        }
    }
}