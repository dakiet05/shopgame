using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xuonggame.Models
{
    public class General
    {
        private static XUONGGAME_DBEntities1 db = new XUONGGAME_DBEntities1();
        public static List<Blog> getBlog()
        {
            List<Blog> listBlog = new List<Blog>();
            listBlog = db.Blogs.ToList<Blog>();
            return listBlog;
        }

        public static List<Product_Category> getProductByCategory(int id)
        {
            List<Product_Category> listProduct = new List<Product_Category>();
            listProduct = db.Product_Category.Where(z => z.CatId == id).ToList();
            return listProduct;
        }
    }
}