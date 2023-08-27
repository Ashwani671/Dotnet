﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank_MVC.Models;
using System.Text.Json;

namespace Bank_MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MvcBankContext _context;

        public CustomersController(MvcBankContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            string? loginCred = null;
            loginCred = HttpContext.Request.Cookies["loginCred"] ?? null;
            if (loginCred != null)
            {
                Customer customer2 = JsonSerializer.Deserialize<Customer>(loginCred);
                return RedirectToAction("Details", new { id = customer2.AccountNumber });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            
            
            Customer customer = null;

            customer = Customer.CustomerLogin(login);
            if (customer == null)
            {
                TempData["msg"] = "Invalid Credentials";
                return RedirectToAction("Login", "Customers");
            }
            string s = JsonSerializer.Serialize(customer);
            HttpContext.Session.SetString("sessionCustomer", s);

            if (login.RememberMe)
            {
                Response.Cookies.Append("loginCred", JsonSerializer.Serialize(customer));
            }
              
            return RedirectToAction("Details",new { id = customer.AccountNumber});
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("loginCred");
            return RedirectToAction("Login");
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            string s = HttpContext.Session.GetString("sessionCustomer");
            if(s != null)
            {
                Customer cust = JsonSerializer.Deserialize<Customer>(s);

                return _context.Customers != null ?
                          View(await _context.Customers.ToListAsync()) :
                          Problem("Entity set 'MvcBankContext.Customers'  is null.");
            }
            
            return RedirectToAction("Login");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,FullName,Username,Password,Balance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountNumber,FullName,Username,Password,Balance")] Customer customer)
        {
            if (id != customer.AccountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.AccountNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'MvcBankContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.AccountNumber == id)).GetValueOrDefault();
        }
    }
}