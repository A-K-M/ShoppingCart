﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Database;

namespace ShoppingCart.Controllers
{
    public class PurchasesController : Controller
    {
        // GET: Purchases
        public ActionResult Index(string PurchaseId)
        {
            List<Purchase> Purchase = PurchaseData.GetPurchaseByPurchaseId(PurchaseId);
            ViewData["Purchase"] = Purchase;
           
            return View();
        }
    }
}