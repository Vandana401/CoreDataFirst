using CoreDataFirst.DB_Context;
using CoreDataFirst.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Userlog = CoreDataFirst.Models.Userlog;

namespace CoreDataFirst.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            List<Employee> model = new List<Employee>();

            coreContext enties = new coreContext();
            var table = enties.TblEmployees;
            foreach (var item in table)
            {
                model.Add(new Employee
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    Department = item.Department,
                    Salary = item.Salary,




                });

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpPost]
        public IActionResult About(Employee model)
        {
            coreContext enties = new coreContext();
            TblEmployee table = new TblEmployee();
            table.Id = model.Id;
            table.Name = model.Name;
            table.Email = model.Email;
            table.Mobile = model.Mobile;
            table.Department = model.Department;
            table.Salary = model.Salary;
            if (model.Id == 0)
            {
                enties.TblEmployees.Add(table);
                enties.SaveChanges();
            }
            else
            {
                enties.Entry(table).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                enties.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            coreContext enties = new coreContext();
            var del = enties.TblEmployees.Where(m => m.Id == id).First();
            enties.TblEmployees.Remove(del);
            enties.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            coreContext enties = new coreContext();
            Employee model = new Employee();
            var edt = enties.TblEmployees.Where(m => m.Id == id).First();
            model.Id = edt.Id;
            model.Name = edt.Name;
            model.Email = edt.Email;
            model.Mobile = edt.Mobile;
            model.Department = edt.Department;
            model.Salary = edt.Salary;
            return View("About", model);


        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult userlogin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult userlogin(Userlog obj)
        {
            coreContext enties = new coreContext();
            var res = enties.Userlogs.Where(m => m.Email == obj.Email).FirstOrDefault();
            if (res == null)
            {
                TempData["wrng"] = "Email is not found";

            }
            else
            {
                if (res.Email == obj.Email&& res.Password == obj.Password)
                {

                    var claims = new[] { new Claim(ClaimTypes.Name, res.Name),
                                        new Claim(ClaimTypes.Email, res.Email )};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));



                    HttpContext.Session.SetString("Name", res.Name
                        );

                    return RedirectToAction("Index");

                }

                else
                {

                    ViewBag.DB = "Wrong password";

                    return View();
                }
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
