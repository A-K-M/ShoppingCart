using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(string Username, string Password)
        {
            if (Username == null)
                return View();

            Customer customer = CustomerData.GetCustomerByUsername(Username);
            if (customer.Password != Password)
                return View();

            string sessionId = SessionData.CreateSession(customer.CustomerId);
            return RedirectToAction("Product", "Gallary", new { sessionId });
        }
    }
}