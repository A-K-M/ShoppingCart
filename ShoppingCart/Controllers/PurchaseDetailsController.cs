using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;

namespace ShoppingCart.Controllers
{
    public class PurchaseDetailsController : Controller
    {
        // GET: PurchaseDetails
        public ActionResult Index(int PurchaseID)
        {
            List<PurchaseDetail> PurchaseDetails = PurchaseDetailsData.GetPurchaseDetailsByPurchaseId(PurchaseID);
            ViewData["PurchaseDetails"] = PurchaseDetails;
            return View();
        }
    }
}