using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;
using DL;
using Serilog;
using System.Web.Providers.Entities;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
     
        private IBL _bl;
        public CustomerController(IBL bl)
        {
            _bl = bl;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            string customer = HttpContext.Request.Cookies["Customer"];
            Log.Information($"{customer} is looking at the list of customers");
            List<Customer> allCustomers = _bl.ListOfCustomers();
            return View(allCustomers);
        }
        public ActionResult Store()
        {
            return RedirectToAction("Index", "Store");
        }


        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            Log.Information("New user signing up");
            try
            {
                if(ModelState.IsValid)
                {
                    _bl.createCustomer(customer);
                    return RedirectToAction(nameof(SignIn));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(Customer customer)
        {
            Customer foundCustomer = _bl.SearchCustomer(customer);
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("../WebUI/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information($"{foundCustomer.Name} signing in");
            if (foundCustomer?.Name == "AdminLogin")
            {
                HttpContext.Response.Cookies.Append("user_id", foundCustomer.Id.ToString());
                HttpContext.Response.Cookies.Append("Customer", foundCustomer.Name.ToString());
                TempData["Username"] = customer.Name;
                TempData.Keep("Username");
                return RedirectToAction("Index", "Store");
            }
            else if(foundCustomer != null)
            {
                HttpContext.Response.Cookies.Append("user_id", foundCustomer.Id.ToString());
                HttpContext.Response.Cookies.Append("Customer", foundCustomer.Name.ToString());
                TempData["Username"] = customer.Name;
                TempData.Keep("Username");
                return RedirectToAction("Index", "OrderDetails");
            }
            else
            {
                return View();
            }
        }
       
        public ActionResult LogOut()
        {
            return View();
        }
       
        //GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new Customer(_bl.GetOneCustomerById(id)));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateCustomer(customer);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new Customer(_bl.GetOneCustomerById(id)));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bl.RemoveCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
