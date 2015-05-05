using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuChi.Models;
namespace WebsiteThuChi.Controllers
{
    public class AnalisysController : Controller
    {
        //
        // GET: /Analisys/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetTable(DateTime startDate, DateTime endDate)
        {
            using (TestDbEntities1 db= new TestDbEntities1())
            {
                decimal totalIn=0,totalOut=0;
                totalIn = db.IncomeLineUps.Where(a => a.Income.IncomeDate >= startDate && a.Income.IncomeDate <= endDate).Sum(a=>a.Asset).Value;
                totalOut = db.OutcomeLineUps.Where(a => a.Outcome.OutcomeDate >= startDate && a.Outcome.OutcomeDate <= endDate).Sum(a => a.Asset).Value;
                return Json(new {tIn=totalIn, tOut=totalOut },JsonRequestBehavior.AllowGet);
            }
        }

    }
}
