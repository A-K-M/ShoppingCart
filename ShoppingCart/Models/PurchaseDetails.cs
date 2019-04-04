﻿using System;
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
        public Product Product { get; set; }
        public Purchase Purchase { get; set; }
        public int Quantity { get; set; }
    }
}