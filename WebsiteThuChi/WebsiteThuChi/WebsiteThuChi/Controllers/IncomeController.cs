using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuChi.Models;
namespace WebsiteThuChi.Controllers
{
    public class IncomeController : Controller
    {
        //
        // GET: /Income/
        TestDbEntities1 db = new TestDbEntities1();
        public ActionResult Index()
        {
           
                 return View(db.Incomes.ToList());
            
        }
        public ActionResult AddIncome()
        {
            ViewBag.Cus = new SelectList(db.Customers, "Id", "CustomerName");
            return View();
        }

        public JsonResult CheckDuplicateIncome(string incomeDate)
        {
        
            using (TestDbEntities1 db = new TestDbEntities1())
            {
                bool isDuplicate = true;
                try
                {
                    DateTime date = DateTime.Parse(incomeDate);
                    isDuplicate = !db.Incomes.Where(e => e.IncomeDate.Value == date).ToList().Any();
                }
                catch
                {
                    isDuplicate = false;
                }
                return Json(new { duplicate = isDuplicate }, JsonRequestBehavior.AllowGet);
            }

          
        }

        [HttpPost]
        public ActionResult AddIncome(string incomeName, string incomeDate, string assets, string notes, string customerId)
        {
            bool success = false;
            try
            {
                DateTime inDate= DateTime.Parse(incomeDate);
                string[] assetsary = assets.Split(';');
                string[] notesary = notes.Split(';');
                string[] customeridary = customerId.Split(';');
                Income income = new Income { IncomeName = incomeName, IncomeDate = inDate };
                db.Incomes.Add(income);
                db.SaveChanges();
                for (int i = 1; i < assetsary.Count() - 1; i++)
                {
                    int cusid= int.Parse(customeridary[i]);
                    IncomeLineUp newline = new IncomeLineUp
                    {
                        Asset = Decimal.Parse(assetsary[i]),
                        Notes = notesary[i],
                        CustomerId = cusid,
                        IncomeId= db.Incomes.FirstOrDefault(a=>a.IncomeDate==inDate).Id
                    };
                    db.IncomeLineUps.Add(newline);
                }
                db.SaveChanges();
                success = true;
            }
            catch (Exception)
            {

                success = false;
            }

            
            return Json(new { success = success});
           
        }

  
        public ActionResult Edit(int ID)
        {
            
            var q = db.IncomeLineUps.Where(a => a.IncomeId == ID).ToList();
            if (q != null && q.Any())
            {
                var model = new IncomeDto(q);
                return View(model);
            }
            return View(new IncomeDto());
        }
        [HttpPost]
        public ActionResult Edit(string incomeID,string lineupid, string incomeName, string assets, string notes, string customerId)
        {
            bool result = false;
            try
            {
                int ID = int.Parse(incomeID);
                var q = db.Incomes.Where(a => a.Id == ID).FirstOrDefault();
                q.IncomeName = incomeName;
                q.ModifiedDate = DateTime.Now.Date;
                string[] lineupidary = lineupid.Split(';');
                string[] assetsary = assets.Split(';');
                string[] notesary = notes.Split(';');
                string[] customeridary = customerId.Split(';');
                for (int i = 1; i < lineupidary.Count() - 1; i++)
                {
                    int LineID = int.Parse(lineupidary[i]);
                    var line = db.IncomeLineUps.Where(a => a.Id == LineID).FirstOrDefault();
                    line.Asset = Decimal.Parse(assetsary[i]);
                    line.Notes = notesary[i];
                    line.CustomerId = int.Parse(customeridary[i]);
                }
                for (int j = lineupidary.Count() - 1; j < assetsary.Count() - 1; j++)
                {
                    int cusid = int.Parse(customeridary[j]);
                    IncomeLineUp newline = new IncomeLineUp
                    {
                        Asset = Decimal.Parse(assetsary[j]),
                        Notes = notesary[j],
                        CustomerId = cusid,
                        IncomeId = ID
                    };
                    db.IncomeLineUps.Add(newline);
                }
                db.SaveChanges();
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }
            
            
            return Json(new {success=result});
        }
        public ActionResult Detail(int ID)
        {
            
            var q = db.IncomeLineUps.Where(a => a.IncomeId == ID).ToList();
            if(q==null)
            {
                q.Add(new IncomeLineUp());
            }
            return View(q);
        }

        public JsonResult IsDuplicatedIncome(string InputDate)
        {
            using(TestDbEntities1 db=new TestDbEntities1())
            {
                 bool isDuplicate = true;
                try
                {
                   DateTime date =DateTime.Parse(InputDate);
                   isDuplicate = !db.Incomes.Where(e => e.IncomeDate.Value == date).ToList().Any();
                }
                catch
                {
                    isDuplicate=false;
                }
                return Json(isDuplicate, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
