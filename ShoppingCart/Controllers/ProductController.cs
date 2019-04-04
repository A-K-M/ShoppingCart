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

        public ActionResult Gallery(string sessionId)
        {
            if(sessionId == null)
                return RedirectToAction("Index", "Login");

            List<Product> products = pd.GetAllProducts();
            Customer customer = CustomerData.GetCustomerBySessionId(sessionId);
            //List<CartDetail> cart = CartData.GetCart(sessionId);

            //int quantity = 0;
            //foreach (var cartItem in cart)
            //{
            //    quantity += cartItem.Quantity;

            //}
            int cartQuantity = CartData.GetCartQuantity(sessionId);

            ViewData["products"] = products;
            ViewData["customer"] = customer;
            ViewData["sessionId"] = sessionId;
            ViewData["cartQuantity"] = cartQuantity;
            return View(products);
        }
    }
}