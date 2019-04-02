using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class PurchaseDetailsController : Controller
    {
        // GET: PurchaseDetails
        public ActionResult Index(int PurchaseID)
        {
            List<PurchaseDetails> PurchaseDetails = PurchaseDetailsData.GetPurchaseDetailsBy(PurchaseID);
            ViewData["PurchaseDetails"] = PurchaseDetails;
            return View();
        }
    }
}