using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuChi.Models;
namespace WebsiteThuChi.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/
        private TestDbEntities1 db = new TestDbEntities1();
        public ActionResult Index()
        {
            
            return View(db.Customers.ToList());
        }
        
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Customer cus)
        {
            if(ModelState.IsValid)
            {
                db.Customers.Add(cus);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        public ActionResult Edit(int ID)
        {
            var q = db.Customers.FirstOrDefault(a => a.Id.Equals(ID));
            Customer cus = new Customer { CustomerName = q.CustomerName, CustomerAddress = q.CustomerAddress, Id = q.Id };

            return View(cus);
        }
        [HttpPost]
        public ActionResult Edit(Customer cus)
        {
            if (ModelState.IsValid)
            {
                var q = db.Customers.FirstOrDefault(a => a.Id.Equals(cus.Id));
                q.CustomerName = cus.CustomerName;
                q.CustomerAddress = cus.CustomerAddress;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        public ActionResult Delete(int ID)
        {
            var q = db.Customers.FirstOrDefault(a => a.Id.Equals(ID));
            Customer cus = new Customer { CustomerName = q.CustomerName, CustomerAddress = q.CustomerAddress, Id = q.Id };

            return View(cus);
           
        }
        [HttpPost]
        public ActionResult Delete(Customer cus)
        {
            if (ModelState.IsValid)
            {
                    var selectedItem = db.Customers.Where(t => t.Id == cus.Id ).FirstOrDefault();
                    db.Customers.Remove(selectedItem);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
