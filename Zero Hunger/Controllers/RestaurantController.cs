using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DTOs;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class RestaurantController : Controller
    {

        [HttpGet]
        [Logged]
        public ActionResult RestrurentDashBoard()
        {
            return View();
        }
        [HttpGet]
        [Logged]
        public ActionResult AddFood()
        {
            return View();
        }
        [Logged]

        [HttpPost]
        public ActionResult AddFood(FoodDTO food)
        {
            if (ModelState.IsValid)
            {
                var username = Session["Name"].ToString();
                var db = new ZeroHungerEntities();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<FoodDTO, Food>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<Food>(food);
                var restrurent = (from Restrurnt in db.Restaurnts
                                  where Restrurnt.R_name == username
                                  select Restrurnt).FirstOrDefault();
                Food food_details = new Food();
                food_details.F_name = data.F_name;
                food_details.F_quantity = data.F_quantity;
                food_details.ExpireDate = data.ExpireDate;
                food_details.R_id = restrurent.R_id;
                food_details.Status = "Pending";
                db.Foods.Add(food_details);

                db.SaveChanges();

                return RedirectToAction("RestrurentDashBoard", "Restaurant");
            }
            return View(food);
        }

        [HttpGet]
        [Logged]
        public ActionResult CheckRequestInfo()
        {
            var username = Session["Name"].ToString();


            var db = new ZeroHungerEntities();
            var restrurent = (from Restrurent in db.Restaurnts
                              where Restrurent.R_name == username
                              select Restrurent).FirstOrDefault();
            DateTime dateTime = DateTime.Now;
            var food1 = (from food in db.Foods
                         where food.R_id == restrurent.R_id
                         select food).ToList();
            foreach (var item in food1)
            {
                if (item.ExpireDate <= dateTime)
                {
                    item.Status = "Expired";
                    db.SaveChanges();
                }
            }
            var food2 = (from food in db.Foods
                         where food.R_id == restrurent.R_id
                         select food).ToList();
            ViewBag.food = food2;
            return View(restrurent);
        }
        [Logged]
        [HttpGet]
        public ActionResult CheckAllRequest()
        {
            var db = new ZeroHungerEntities();

            var food = db.Foods.ToList();
            var restrurent = db.Restaurnts.ToList();
            ViewBag.restrurent = restrurent;

            return View(food);
        }
        [Logged]
        [HttpGet]
        public ActionResult CancelRequest(int Id)
        {
            var db = new ZeroHungerEntities();

            var data = db.Foods.Find(Id);

            if (data != null)
            {
                data.Status = "Cancelled By restaurant";
                db.SaveChanges();
            }
            return RedirectToAction("CheckRequestInfo");
        }

        [Logged]
        [HttpGet]
        public ActionResult CancelRequestByEmployee(int Id)
        {
            var db = new ZeroHungerEntities();

            var data = db.Foods.Find(Id);

            if (data != null)
            {
                data.Status = "Cancelled By Employee";
                db.SaveChanges();
            }
            return RedirectToAction("CheckAllRequest");
        }



    }
}