using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult ViewCart(string sessionId)
        {
            // get cust id to match cust id w cart id
            Customer customer = CustomerData.GetCustomerBySessionId(sessionId);
            List<CartDetail> cart = CartData.GetCart(customer.CustomerId);
            ProductData pd = new ProductData();
            List<Product> products = pd.GetAllProducts();
            foreach (var cartitem in cart)
            {
                cartitem.Product = CartData.GetProductByProductId(cartitem.ProductId);
            }
            int cartQuantity = CartData.GetCartQuantity(customer.CustomerId);

            //ViewData["customer"] = customer;
            ViewData["SessionId"] = sessionId;
            ViewData["cart"] = cart;
            ViewData["products"] = products;
            ViewData["cartQuantity"] = cartQuantity;
            ViewData["cid"] = customer.CustomerId;
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




        [HttpPost]
        public ActionResult CheckOut(int CustomerId)
        {
            int P_ID, Qty;

            //create New Purchase Record
            PurchaseData.CreateNewPurchase(CustomerId);
            foreach (string key in Request.Form.AllKeys)
            {
                //Debug.WriteLine("Keys : : : " + key);
                //Debug.WriteLine("Value : : : " + Request[key]);
                if (key != "CheckOut")
                {
                    P_ID = Convert.ToInt32(key);
                    Qty = Convert.ToInt32(Request[key]);

                    for (int i = 0; i < Qty; i++)
                    {
                        PurchaseData.InsertNewPurchses(P_ID);
                    }
                }
            }
            //delete form Cart
            CartData.ClearCart(CustomerId);
            Customer customer_data = CustomerData.GetCustomerByCustomerId(CustomerId);
            string session = customer_data.SessionId;

            return RedirectToAction("Index", "Purchases", new { sessionId = @session });

        }
    }
}