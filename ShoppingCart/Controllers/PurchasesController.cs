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
            if (SessionId == null)
                return RedirectToAction("Index", "Login");
            Customer C = CustomerData.GetCustomerBySessionId(SessionId);
            List<Purchase> PurchaseDetails = PurchaseData.GetPurchaseDetailsByCustomerId(C.CustomerId);
            int cartQuantity = CartData.GetCartQuantity(C.CustomerId);

            ViewData["PurchaseDetails"] = PurchaseDetails;
            ViewData["sessionId"] = SessionId;
            ViewData["cartQuantity"] = cartQuantity;

            return View();
        }
    }
}