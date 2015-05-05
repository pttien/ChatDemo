using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebsiteThuChi.Models;

namespace WebsiteThuChi.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        TestDbEntities1 db = new TestDbEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExportToExcel()
        {
            string excelName = DateTime.Now.ToShortDateString();
            
            using (ExcelPackage p = new ExcelPackage())
            {
                var path = HttpContext.Server.MapPath("~") + "/Export/" +
                    string.Format(@"{0}.xlsx", Guid.NewGuid());
                var model = db.IncomeLineUps.ToList().OrderBy(date=>date.Income.IncomeDate);


   

                p.Workbook.Properties.Author = "Income";
                p.Workbook.Properties.Title = excelName;
                p.Workbook.Properties.Company = "FiRM";
                p.Workbook.Worksheets.Add(excelName);
 
                ExcelWorksheet ws = p.Workbook.Worksheets[1];
                ws.Protection.IsProtected = false;
                ws.Cells[1, 1].Value = "Date";
                ws.Cells[1, 2].Value = "Tien Thu";
                ws.Cells[1, 3].Value = "Note";
                ws.Cells[1, 4].Value = "Khach Hang";
        
             
                int wsRowIndex = 2;
                foreach( var item in model)
                {
                    for(int i=1;i<5;i++)
                    {
                        ws.Cells[wsRowIndex, i].Value = 
                                i == 1 ? string.Format("{0:dd/MM/yyyy}",item.Income.IncomeDate) : i == 2 ? item.Asset.ToString() :
                               i == 3 ? item.Notes : i == 4 ? item.Customer.CustomerName : "";

                    }
                    wsRowIndex++;
                }

                Byte[] bin = p.GetAsByteArray();



                System.IO.File.WriteAllBytes(path, bin);
                return File(path, "application/vnd.ms-excel", string.Format("{0}_{1}.xlsx", Guid.NewGuid(), excelName));
            }
        }
        public ActionResult ExportToExcel1()
        {
            string excelName = DateTime.Now.ToShortDateString();

            using (ExcelPackage p = new ExcelPackage())
            {
                var path = HttpContext.Server.MapPath("~") + "/Export/" +
                    string.Format(@"{0}.xlsx", Guid.NewGuid());
                var model = db.OutcomeLineUps.ToList().OrderBy(date=>date.Outcome.OutcomeDate);


                p.Workbook.Properties.Author = "Outcome";
                p.Workbook.Properties.Title = excelName;
                p.Workbook.Properties.Company = "FiRM";
                p.Workbook.Worksheets.Add(excelName);

                ExcelWorksheet ws = p.Workbook.Worksheets[1];
                ws.Protection.IsProtected = false;
                ws.Cells[1, 1].Value = "Date";
                ws.Cells[1, 2].Value = "Tien Chi";
                ws.Cells[1, 3].Value = "Note";
                ws.Cells[1, 4].Value = "Khach Hang";


                int wsRowIndex = 2;
                foreach (var item in model)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        ws.Cells[wsRowIndex, i].Value =
                                i == 1 ? string.Format("{0:dd/MM/yyyy}", item.Outcome.OutcomeDate) : i == 2 ? item.Asset.ToString() :
                               i == 3 ? item.Notes : i == 4 ? item.Customer.CustomerName : "";

                    }
                    wsRowIndex++;
                }

                Byte[] bin = p.GetAsByteArray();



                System.IO.File.WriteAllBytes(path, bin);
                return File(path, "application/vnd.ms-excel", string.Format("{0}_{1}.xlsx", Guid.NewGuid(), excelName));
            }
        }

    }
}
