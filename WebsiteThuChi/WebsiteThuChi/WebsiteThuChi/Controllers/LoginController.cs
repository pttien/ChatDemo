using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuChi.Models;

namespace WebsiteThuChi.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        //POST: 
        [HttpPost]
        public ActionResult Index(Account user)
        {
            if(ModelState.IsValid)
            {
                using(TestDbEntities1 db= new TestDbEntities1())
                {
                    var q= db.Accounts.Where(a=>a.Username.Equals(user.Username)&& a.Password.Equals(user.Password)).FirstOrDefault();
                    if(q!=null)
                    {
                        Session["Username"]= q.Username;
                        Session["Fullname"]=q.FullName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        string meg = "Tài khoản không đúng!!!";
                        ViewBag.Meg = meg;
                        return View();
                    }
                }
            }
            return View();
        }

    }
}
