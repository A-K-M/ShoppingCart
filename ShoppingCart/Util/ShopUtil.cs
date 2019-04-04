
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ShoppingCart.Util
{
    public class ShopUtil
    {
        public static List<string> GetActivationList(string actcode)
        {
            if(actcode == null)
            {
                return new List<string>();
            }
            string[] actcodes = actcode.Split(new char[] { ',' });
            return actcodes.ToList<string>();
        }

        public static List<SelectListItem> GetActivationSelectList(string actcode)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (actcode == null)
            {
                return items;
            }

            string[] actcodes = actcode.Split(new char[] { ',' });
            return actcodes.Select(s => new SelectListItem { Value = s }).ToList();
        }
        public static string GenerateActivationCodeByQuantity(int qty)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < qty; i++)
            {
                sb.Append(GenerateActCode());
                if (i < qty - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        private static string GenerateActCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 17);
        }
    }
}