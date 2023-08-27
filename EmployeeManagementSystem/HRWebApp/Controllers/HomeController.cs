using HRWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BOL;
using System.Collections.Generic;
using DAL;
using System.Security.Cryptography.X509Certificates;
using System;
using Microsoft.VisualBasic;
using System.Globalization;

namespace HRWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Employees() {

            List<Employee> empList = DBManager.getAllEmployees();

            ViewBag.ListOfEmployees = empList;
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(string id, string empname, string designation,
            string department, string city, string salary, string joiningdate) {

            Employee newEmp = new Employee(int.Parse(id), empname, designation,
                Enum.Parse<Department>(department),
                city, double.Parse(salary), DateOnly.Parse(joiningdate));

             DBManager.insertEmployee(newEmp);

            return RedirectToAction("Employees");
        }


        public IActionResult Register() {

            return View();
        }

        public IActionResult Delete() { 
            
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id) {

            DBManager.deleteEmployee(id);

            return RedirectToAction("Employees");
        }


        public IActionResult GetByID(int id) {

            Employee foundEmp = DBManager.GetEmployeeById(id);

            if (foundEmp != null)
            {
                ViewBag.FOUNDEMP = foundEmp;
                return View();
            }
            return RedirectToAction("Index");
        
        }

        public IActionResult Update(string id, string empname, string designation,
            string department, string city, string salary, string joiningdate) {

            Employee updatedEmp = new Employee(int.Parse(id), empname, designation, Enum.Parse<Department>(department), 
                city,double.Parse(salary), DateOnly.Parse(joiningdate));

            Console.WriteLine(updatedEmp);
            DBManager.updateEmployee(updatedEmp);

            return RedirectToAction("Employees");        
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}