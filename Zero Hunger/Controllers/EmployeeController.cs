using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeDashBoard()
        {
            return View();
        }

        [Logged]
        [HttpGet]
        public ActionResult SeeAllRequest()
        {
            var db = new ZeroHungerEntities();

            var food = db.Foods.ToList();
            var restrurent = db.Restaurnts.ToList();
            ViewBag.restrurent = restrurent;



            return View(food);
        }
        [HttpGet]
        public ActionResult AcceptRequestByEmployee(int Id)
        {
            var db = new ZeroHungerEntities();

            var data = db.Foods.Find(Id);

            if (data != null)
            {
                data.Status = "Accpted";
                db.SaveChanges();
            }
            return RedirectToAction("SeeAllRequest");
        }
    }
}