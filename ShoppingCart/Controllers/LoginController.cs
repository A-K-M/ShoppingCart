using System;
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

            if (customer.Password != hashPW)
                return View();

            string sessionId = SessionData.CreateSession(customer.CustomerId);

            //string sessionId = Guid.NewGuid().ToString();
            Session[sessionId] = Username;
            Session[Username] = sessionId;

            ViewData["cartQuantity"] = 0;
            ViewData["customer"] = customer;
            return RedirectToAction("Gallery", "Product", new { sessionId });


            string GetMd5Hash(MD5 md5Hash, string input)
            {

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}