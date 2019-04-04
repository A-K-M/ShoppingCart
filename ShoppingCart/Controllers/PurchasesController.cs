using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;

namespace ShoppingCart.Controllers
{
    public class PurchasesController : Controller
    {
        // GET: Purchases
        public ActionResult Index(string SessionId)
        {
            List<PurchaseDetails> PurchaseDetails = PurchaseDetailsData.GetPurchaseDetailsBySessionId(SessionId);
            foreach (var purchase in PurchaseDetails)
            {
                purchase.Purchase = PurchaseDetailsData.GetPurchaseByPurchaseId(purchase.PurchaseId);
                purchase.Product = PurchaseDetailsData.GetProductByProductId(purchase.ProductId);
            }

            ViewData["PurchaseDetails"] = PurchaseDetails;
            ViewData["sessionId"] = SessionId;
          
            return View();
        }
    }
}