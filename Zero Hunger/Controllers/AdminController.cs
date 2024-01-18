using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DTOs;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        [HttpGet]
        [Logged]
        public ActionResult AdminDashBoard()
        {
            var db = new ZeroHungerEntities();
            ViewBag.Employee = db.Employees.ToList();
            var food = db.Foods.ToList();
            var restrurent = db.Restaurnts.ToList();
            ViewBag.restrurent = restrurent;
            return View(food);
        }
        [HttpPost]
        public ActionResult Order()
        {
            var db = new ZeroHungerEntities();

            return RedirectToAction("SeeAllRequest", "Employee");
        }

        [HttpGet]
        [Logged]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [Logged]

        [HttpPost]
        public ActionResult AddEmployee(EmployeeDTO emp)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ZeroHungerEntities())
                {
                    // Create a new User
                    var user = new User
                    {
                        Name = emp.E_name,
                        Password = emp.Password,
                        UserType = "Employee"
                    };

                    // Create a new Employee associated with the User
                    var employee = new Employee
                    {
                        E_name = emp.E_name,
                        E_address = emp.E_address,
                        E_phone = emp.E_phone,
                        E_gender = emp.E_gender,
                        // Associate the User with the Employee
                    };

                    // Add both User and Employee to the database
                    db.Users.Add(user);
                    db.Employees.Add(employee);

                    // Save changes to the database
                    db.SaveChanges();

                    return RedirectToAction("AdminDashBoard", "Admin");
                }

            }
                
            
            return View(emp);
        }

        //public User convert(EmployeeDTO s)
        //{
        //    var st = new User()
        //    {
        //        Name = s.E_name,
        //        Password = s.Password,
        //        UserType = "Employee"
        //    };
        //    return st;
        //}


        [Logged]
        [HttpGet]

        public ActionResult EditEmployee(int Id)
        {
            var db = new ZeroHungerEntities();
            var data = db.Employees.Find(Id);

            if (data == null)
            {
                // Handle the case where the employee with the specified Id was not found.
                TempData["ErrorMessage"] = "Employee not found!";
                return HttpNotFound(); // or return RedirectToAction("SomeErrorAction", "SomeController");
            }

            return View(data);
        }
        [Logged]
        [HttpPost]
        public ActionResult EditEmployee(Employee d)
        {
            var db = new ZeroHungerEntities();
            var ex = db.Employees.Find(d.E_id);
            ex.E_name = d.E_name;
            ex.E_address = d.E_address;
            ex.E_phone = d.E_phone;
            ex.E_gender = d.E_gender;
            db.SaveChanges();
            return RedirectToAction("AdminDashBoard", "Admin");
        }
        //[Logged]
        //[HttpGet]
        //public ActionResult EditEmployee(int id)
        //{
        //    var db = new ZeroHungerEntities();
        //    var employee = db.Employees.Find(id);

        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var empDTO = new Employee
        //    {
        //        E_id = employee.E_id,
        //        E_name = employee.E_name,
        //        E_address = employee.E_address,
        //        E_phone = employee.E_phone,
        //        E_gender = employee.E_gender
        //    };

        //    return View(empDTO);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditEmployee(Employee emp)
        //{
        //    if (ModelState.IsValid)
        //    {

        //            var db = new ZeroHungerEntities();

        //            var existingEmployee = db.Employees.Find(emp.E_id);

        //            if (existingEmployee != null)
        //            {
        //                existingEmployee.E_name = emp.E_name;
        //                existingEmployee.E_address = emp.E_address;
        //                existingEmployee.E_phone = emp.E_phone;
        //                existingEmployee.E_gender = emp.E_gender;

        //                db.SaveChanges();

        //                TempData["SuccessMessage"] = "Employee details updated successfully!";
        //                return RedirectToAction("AdminDashBoard", "Admin");
        //            }



        //    }


        //    return View(emp);
        //}


        public ActionResult DeleteEmployee(int id)
        {
            var db = new ZeroHungerEntities();
            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeleteEmployee(int id)
        {
            var db = new ZeroHungerEntities();
            var employee = db.Employees.Find(id);

            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }

            return RedirectToAction("AdminDashBoard", "Admin");
        }
    }
}