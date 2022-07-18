using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Security;
namespace WebApplication1.Controllers
    
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model)

        {
            using(var context = new lindaaEntities())
            {
                bool isValid = context.User1.Any(x => x.Username == model.Username && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "courses");
                }
                ModelState.AddModelError("", "Invalid username and password");
            }

            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User1 model)

        {
            using(var context = new lindaaEntities())
            {
                context.User1.Add(model);
                context.SaveChanges();
            }    
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}