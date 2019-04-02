using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        ProductData pd = new ProductData();
        // GET: Product
        public ActionResult Gallery()
        {
            List<Product> products = pd.GetAllProducts();
            return View(products);
        }
    }
}