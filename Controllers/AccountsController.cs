using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using xuonggame.Models;

namespace xuonggame.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(UserModel model)
        {
            using (XUONGGAME_DBEntities1 context = new XUONGGAME_DBEntities1())
            {
                model.UserPassword = GetMD5(model.UserPassword);
                bool IsValidUser = context.Users.Any(user => user.UserName.ToLower() == model.UserName.ToLower() && user.UserPassword == model.UserPassword);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees", new { area = "ADMIN" });
                }
                ModelState.AddModelError("", "invalid Username or Password");
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Signup(User model)
        {
            using (XUONGGAME_DBEntities1 context = new XUONGGAME_DBEntities1())
            {
                model.UserPassword = GetMD5(model.UserPassword);
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login");
        }
        public static string GetMD5(string str)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = sha256.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}