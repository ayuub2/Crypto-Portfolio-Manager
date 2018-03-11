using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CryptoMonitor.Models.Login;

namespace CryptoMonitor.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(User user)
        {
            using (CryptoMonitorEntities db = new CryptoMonitorEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    user.LoginErrorMessage = "Wrong username or password";
                    return View("Index", user);
                }
                else
                {
                    Session["userId"] = userDetails.UserId;
                    return RedirectToAction("Index", "Dashboard", userDetails);
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}