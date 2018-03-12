using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Crypto_Portfolio_Manager.Controllers;

namespace CryptoMonitor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            BittrexManager manager = new BittrexManager("40f1ca9a1ad74913a1ac90f181418c1b", "d096de9c9238433787b8c36bee917a5c");
            try
            {
                var result = await manager.GetOrderHistory();
                System.Diagnostics.Debug.WriteLine("Result: " + result.Result);
                System.Diagnostics.Debug.WriteLine("Success: " + result.Success, "Message" + result.Message);
                var displayResult = result.Result[0];
                //return View(displayResult);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + e);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}