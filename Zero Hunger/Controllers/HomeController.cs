using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.DTOs;
using Zero_Hunger.EF;
using Zero_Hunger.Auth;

namespace Zero_Hunger.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<LoginDTO, User>();
                });

                var mapper = new Mapper(config);
                var data = mapper.Map<User>(login);

                using (var db = new ZeroHungerEntities())
                {
                    var user = db.Users.FirstOrDefault(u => u.Name == data.Name && u.Password == data.Password);

                    if (user != null)
                    {
                        Session["Name"] = user.Name;

                        if (user.UserType.Equals("Admin"))
                        {
                            return RedirectToAction("AdminDashBoard", "Admin");
                        }
                        else if (user.UserType.Equals("Restaurant"))
                        {
                            return RedirectToAction("RestrurentDashBoard", "Restaurant");
                        }
                        else if (user.UserType.Equals("Employee"))
                        {
                            return RedirectToAction("EmployeeDashBoard", "Employee");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Username and password do not match.");
                    }
                }
            }

            return View(login);
        }


        public ActionResult Registration()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationDTO signUp)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ZeroHungerEntities())
                {
                    var user = new User
                    {
                        Name = signUp.R_name,
                        Password = signUp.Password,
                        UserType = "Restaurant"
                    };

                    var restaurant = new Restaurnt
                    {
                        R_name = signUp.R_name,
                        R_address = signUp.R_address,
                        R_ownername = signUp.R_ownername,
                    };
                    db.Users.Add(user);
                    db.Restaurnts.Add(restaurant);
                    db.SaveChanges();

                    return RedirectToAction("Login", "Home");
                }
            }
            return View(signUp);
        }
        //[HttpPost]
        //public ActionResult Registration(SignupDTO signUp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var db = new ZeroHungerEntities();
        //        db.Users.Add(Convert(signUp));
        //        db.SaveChanges();
        //        var data = new Restaurnt();
        //        data.R_name = signUp.R_name;
        //        data.R_address = signUp.R_address;
        //        data.R_ownername = signUp.R_ownername;

        //        db.Restaurnts.Add(data);
        //        db.SaveChanges();

        //        return RedirectToAction("Login", "Home");
        //    }
        //    return View(signUp);
        //}
        //public User Convert(SignupDTO s)
        //{
        //    var st = new User()
        //    {
        //        Name = s.R_name,
        //        Password = s.Password,
        //        UserType = "Restrurent"
        //    };
        //    return st;
        //}








    }
}