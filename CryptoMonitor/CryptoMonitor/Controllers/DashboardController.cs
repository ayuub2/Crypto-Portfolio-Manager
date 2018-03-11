using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CryptoMonitor.Models.Login;

namespace CryptoMonitor.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(User user)
        {
            return View(user);
        }
    }
}