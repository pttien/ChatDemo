using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThuChi.Models
{
    public static class LookupViewModel
    {
     

        public static List<SelectListItem> GetCustomerList()
        {
            using(TestDbEntities1 db = new TestDbEntities1())
            {
                var list = new List<SelectListItem>();
                var customers = db.Customers.ToList();
                if (customers != null && customers.Any())
                {
                    foreach (var cus in customers)
                    {
                        list.Add(new SelectListItem { Text = cus.CustomerName, Value = cus.Id.ToString() });
                    }
                    return list;
                }
              
               list.Add(new SelectListItem { Text ="No Customer Found",Value = "0" });
               return list;
            }
        }
        public static List<SelectListItem> GetCustomerList(string ID)
        {
            var list = GetCustomerList();
            foreach (var cus in list)
            {
                if(cus.Value==ID)
                {
                    cus.Selected = true ;
                }
            }
            
            return list;
        }
    }
}