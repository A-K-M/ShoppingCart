using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;
using System.Data.SqlClient;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult ViewCart(string sessionId)
        {
            // get cust id to match cust id w cart id
            Customer customer = CustomerData.GetCustomerBySessionId(sessionId);
            List<CartDetail> cart = CartData.GetCart(sessionId);
            ProductData pd = new ProductData();
            List<Product> products = pd.GetAllProducts();
            foreach (var cartitem in cart)
            {
                cartitem.Product = CartData.GetProductByProductId(cartitem.ProductId);
            }
            int cartQuantity = CartData.GetCartQuantity(sessionId);

            ViewData["customer"] = customer;
            ViewData["SessionId"] = sessionId;
            ViewData["cart"] = cart;
            ViewData["products"] = products;
            ViewData["cartQuantity"] = cartQuantity;
            return View();
        }

        public ActionResult AddToCart(int ProductId, int CustomerId, string sessionId)
        {
            CartData.AddToCart(ProductId, CustomerId, sessionId);
            return RedirectToAction("Gallery", "Product", new { sessionId = @sessionId });
        }
    }
}