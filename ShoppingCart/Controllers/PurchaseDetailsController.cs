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
        public ActionResult Index(string sessionId)
        {
            List<PurchaseDetails> PurchaseDetails = PurchaseDetailsData.GetPurchaseDetailsBySessionId(sessionId);
            List<Purchase> Purchase = PurchaseData.GetPurchaseBySessionId(sessionId);
            ProductData pd = new ProductData();
            List<Product> products = pd.GetAllProducts();
            ViewData["PurchaseDetails"] = PurchaseDetails;
            ViewData["sessionId"] = sessionId;
            ViewData["Purchase"] = Purchase;
            ViewData["products"] = products;
            return View();
        }
    }
}