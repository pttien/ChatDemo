using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuChi.Models;
namespace WebsiteThuChi.Controllers
{
    public class OutcomeController : Controller
    {
        //
        // GET: /Outcome/

        public ActionResult Index()
        {
            using (TestDbEntities1 db = new TestDbEntities1())
            {
                var q = db.Outcomes.ToList();

                List<OutcomeDto> list = new List<OutcomeDto>();
                foreach (var item in q)
                {
                    list.Add(new OutcomeDto
                    {
                        Id = item.Id,
                        OutcomeDate = item.OutcomeDate,
                        OutcomeName = item.OutcomeName,
                        ModifiedDate = item.ModifiedDate
                    });
                }
                return View(list.ToList());
            }

        }
        public ActionResult AddOutcome()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOutcome(string outcomeName, string outcomeDate,string notes, string assets, string customerID)
        {
            bool result = false;
            DateTime date;
            DateTime.TryParse(outcomeDate,out date);
            string[] assetsary = assets.Split(';');
            string[] notesary = notes.Split(';');
            string[] customeridary = customerID.Split(';');
            using (TestDbEntities1 db= new TestDbEntities1() )
            {
                Outcome outcome = new Outcome { OutcomeName = outcomeName, OutcomeDate = date };
                db.Outcomes.Add(outcome);
                db.SaveChanges();
                for (int i = 1; i < assetsary.Count() - 1; i++)
                {
                    int cusid = int.Parse(customeridary[i]);
                    OutcomeLineUp newline = new OutcomeLineUp
                    {
                        Asset = Decimal.Parse(assetsary[i]),
                        Notes = notesary[i],
                        CustomerId = cusid,
                        OutcomeId = db.Outcomes.FirstOrDefault(a => a.OutcomeDate == date).Id
                    };
                    db.OutcomeLineUps.Add(newline);
                }
                db.SaveChanges();
                result = true;
            }
            return Json (new{result= result});
        }
        public ActionResult Detail(int ID)
        {
            using (TestDbEntities1 db = new TestDbEntities1()) { 
                var q = db.OutcomeLineUps.Where(a => a.OutcomeId == ID).ToList();
                if (q != null && q.Any())
                {
                    var model = new OutcomeDto(q);
                    return View(model);
                }
                return View(new OutcomeDto());
            }
        }
        public ActionResult Edit(int ID)
        {
            using (TestDbEntities1 db = new TestDbEntities1())
            {
                var q = db.OutcomeLineUps.Where(a => a.OutcomeId == ID).ToList();
                if (q != null && q.Any())
                {
                    var model = new OutcomeDto(q);
                    return View(model);
                }
                return View(new OutcomeDto());
            }
        }
        [HttpPost]
        public ActionResult Edit(string outcomeID, string lineupid, string outcomeName, string assets, string notes, string customerId)
        {
           using(TestDbEntities1 db= new TestDbEntities1())
           {
               bool result = false;
               try
               {
                   int ID = int.Parse(outcomeID);
                   var q = db.Outcomes.Where(a => a.Id == ID).FirstOrDefault();
                   q.OutcomeName = outcomeName;
                   q.ModifiedDate = DateTime.Now.Date;
                   string[] lineupidary = lineupid.Split(';');
                   string[] assetsary = assets.Split(';');
                   string[] notesary = notes.Split(';');
                   string[] customeridary = customerId.Split(';');
                   for (int i = 1; i < lineupidary.Count() - 1; i++)
                   {
                       int LineID = int.Parse(lineupidary[i]);
                       var line = db.OutcomeLineUps.Where(a => a.Id == LineID).FirstOrDefault();
                       line.Asset = Decimal.Parse(assetsary[i]);
                       line.Notes = notesary[i];
                       line.CustomerId = int.Parse(customeridary[i]);
                   }
                   for (int j = lineupidary.Count() - 1; j < assetsary.Count() - 1; j++)
                   {
                       int cusid = int.Parse(customeridary[j]);
                       OutcomeLineUp newline = new OutcomeLineUp
                       {
                           Asset = Decimal.Parse(assetsary[j]),
                           Notes = notesary[j],
                           CustomerId = cusid,
                           OutcomeId = ID
                       };
                       db.OutcomeLineUps.Add(newline);
                   }
                   db.SaveChanges();
                   result = true;
               }
               catch (Exception)
               {

                   result = false;
               }


               return Json(new { result = result });
           }
        }
        public JsonResult CheckDuplicate(string outcomeDate)
        {

            using (TestDbEntities1 db = new TestDbEntities1())
            {
                bool isDuplicate = true;
                try
                {
                    DateTime date = DateTime.Parse(outcomeDate);
                    isDuplicate = !db.Outcomes.Where(e => e.OutcomeDate.Value == date).ToList().Any();
                }
                catch
                {
                    isDuplicate = false;
                }
                return Json(new { duplicate = isDuplicate }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult IsDuplicatedOutcome(DateTime OutcomeDate)
        {
            using (TestDbEntities1 db = new TestDbEntities1())
            {
                bool isDuplicate = true;
                try
                {
                    
                    isDuplicate = !db.Outcomes.Where(e => e.OutcomeDate.Value == OutcomeDate).ToList().Any();
                }
                catch
                {
                    isDuplicate = false;
                }
                return Json(isDuplicate, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
