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

            int cartQuantity = CartData.GetCartQuantity(SessionId);
            
            ViewData["PurchaseDetails"] = PurchaseDetails;
            ViewData["sessionId"] = SessionId;
            ViewData["cartQuantity"] = cartQuantity;

            return View();
        }
    }
}