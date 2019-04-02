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
            if (Session[Username] != null)
                return RedirectToAction("Gallery", "Product", Session[Username]);

            if (Username == null)
                return View();

            Customer customer = CustomerData.GetCustomerByUsername(Username);
            if (customer.Password != Password)
                return View();

            string sessionId = SessionData.CreateSession(customer.CustomerId);

            //string sessionId = Guid.NewGuid().ToString();

            Session[sessionId] = Username;
            Session[Username] = sessionId;


            return RedirectToAction("Gallery", "Product", new { sessionId });
        }
    }
}