using System;
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
            BittrexManager manager = new BittrexManager();
            var coinSummaryReq = await manager.GetCoins();
            var coinSummaryResult = coinSummaryReq.Result[0];

            return View(coinSummaryResult);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}