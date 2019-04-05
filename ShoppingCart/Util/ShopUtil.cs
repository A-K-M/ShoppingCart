
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ShoppingCart.Util
{
    public class ShopUtil
    {
        public static string GenerateActCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 17);
        }
    }
}