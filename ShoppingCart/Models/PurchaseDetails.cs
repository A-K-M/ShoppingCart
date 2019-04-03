using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class PurchaseDetails
    {
        public int PurchaseId {get; set;}
        public int ProductId {get; set;}
        public string ActivationCode {get; set;}
    }
}