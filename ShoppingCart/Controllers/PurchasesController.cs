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
            int currentCustomer = 0;
            List<PurchaseDetails> PurchaseDetails = PurchaseDetailsData.GetPurchaseDetailsBySessionId(SessionId);
            List<Purchase> Purchase = PurchaseData.GetPurchaseBySessionId(SessionId);

            foreach (var purchase in PurchaseDetails)
            {
                purchase.Purchase = PurchaseDetailsData.GetPurchaseByPurchaseId(purchase.PurchaseId);
                purchase.Product = PurchaseDetailsData.GetProductByProductId(purchase.ProductId);
                foreach (var i in Purchase)
                {
                    if (i.PurchaseId == purchase.PurchaseId)
                    { currentCustomer = i.CustomerId; }
                }
                purchase.Quantity = PurchaseDetailsData.getpurchasedproductscount(currentCustomer, purchase.ProductId);
            }

            ViewData["PurchaseDetails"] = PurchaseDetails;
            ViewData["sessionId"] = SessionId;
          
            return View();
        }
    }
}