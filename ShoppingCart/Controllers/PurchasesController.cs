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
            List<Purchase> Purchase = PurchaseData.GetPurchaseBySessionId(SessionId);
            ProductData pd = new ProductData();
            List<Product> products = pd.GetAllProducts();
            ViewData["PurchaseDetails"] = PurchaseDetails;
            ViewData["sessionId"] = SessionId;
            ViewData["Purchase"] = Purchase;
            ViewData["products"] = products;
           
            return View();
        }
    }
}