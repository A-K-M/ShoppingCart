using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Models
{
    public class PurchaseDetails
    {
        public int PurchaseId {get; set;}
        public int ProductId {get; set;}
        public List<string> ActivationCodes {get; set;}
        public Product Product { get; set; }
    }
}