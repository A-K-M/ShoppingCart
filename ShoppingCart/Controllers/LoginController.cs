﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;
using System.Security.Cryptography;
using System.Text;
using ShoppingCart.Hash;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(string Username, string Password)
        {
            string hashPW = null;
            //if (Session[Username] != null)
            //    return RedirectToAction("Gallery", "Product", Session[Username]);

            if (Username == null)
                return View();

            Customer customer = CustomerData.GetCustomerByUsername(Username);

            using (MD5 md5Hash = MD5.Create())
            {
                hashPW = MD5Hash.GetMd5Hash(md5Hash, Password);
            }

            if (customer.Password != hashPW) {
                TempData["error"] = "<script>alert('Login Failed! Try Again!');</script>";
                return View();
            }
                
            string sessionId = SessionData.CreateSession(customer.CustomerId);
            Session[sessionId] = Username;
            Session[Username] = sessionId;
            ViewData["cartQuantity"] = 0;
            ViewData["customer"] = customer;
            return RedirectToAction("Gallery", "Product", new { sessionId });           
        }
    }
}