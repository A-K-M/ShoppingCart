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
            if (sessionId == null)
                return RedirectToAction("Index", "Login");
            Customer customer = CustomerData.GetCustomerBySessionId(sessionId);
            List<CartDetail> cart = CartData.GetCart(customer.CustomerId);
            ProductData pd = new ProductData();
            List<Product> products = pd.GetAllProducts();
            foreach (var cartitem in cart)
            {
                cartitem.Product = CartData.GetProductByProductId(cartitem.ProductId);
            }
            int cartQuantity = CartData.GetCartQuantity(customer.CustomerId);

            ViewData["SessionId"] = sessionId;
            ViewData["cart"] = cart;
            ViewData["products"] = products;
            ViewData["cartQuantity"] = cartQuantity;
            return View();
        }

        public ActionResult AddToCart(int ProductId, int CustomerId)
        {
            CartData.AddToCart(ProductId, CustomerId);
            Customer customer = CustomerData.GetCustomerByCustomerId(CustomerId);
            string sessionId = customer.SessionId;
            return RedirectToAction("Gallery", "Product", new { sessionId = @sessionId });
        }

        //public int AddToCartAjax(int productId, int customerId)
        //{
        //    CartData.AddToCart(productId, customerId);
        //    return CartData.GetCartQuantity(customerId);
        //}
        
        //public ActionResult CheckOut(int CustomerId)
        //{
        //    //insert into Purhcase 


        //    //delete form Cart
        //    string session = CustomerData.GetSessionId(CustomerId);
        //    CartData.ClearCart(CustomerId);
        //    return RedirectToAction("Gallery", "Product", new { sessionId = session });
        //}
    }
}