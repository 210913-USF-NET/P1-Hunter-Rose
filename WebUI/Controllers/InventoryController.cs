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

namespace WebUI.Controllers
{
    public class InventoryController : Controller
    {
        
        private IBL _bl;
        public InventoryController(IBL bl)
        {
            _bl = bl;
        }
        // GET: InventoryController
        public ActionResult Index()
        {
            List<Inventory> allInventories = _bl.GetInventory();
            return View(allInventories);
        }
        [HttpPost]
        public ActionResult Coupon(string coupon)
        {
            double coup1 = .8;
            TempData["Coupon"] = ".8";
            TempData.Keep("Coupon");
            HttpContext.Response.Cookies.Append("Coupon", coup1.ToString());
            return new EmptyResult();
        }
        public ActionResult Store()
        {
            return RedirectToAction("Index", "Store");
        }

        // GET: InventoryController/Details/5
        public ActionResult StoreIndex(int id)
        {
            List<Inventory> allInventories = _bl.GetInventorybyStoreId(id);
            return View(allInventories);
        }

        public ActionResult newOrder(int id)
        {
            List<Stores> store = _bl.StoreLocation();
            for(int i = 0; i <store.Count; i++)
            {
                if(id == store[i].Id)
                {
                    string location = store[i].Location;
                    string customer = HttpContext.Request.Cookies["Customer"];
                    Log.Information($"{customer} chose store location {location}");
                }
            }
            List<Inventory> allInventories = _bl.GetInventorybyStoreId(id);
            return View(allInventories);
        }
        // GET: InventoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory inventory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.createInventory(inventory);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Purchase(int id)
        {
            return View(new Inventory(_bl.GetOneInventoryById(id)));
        }

        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(int id, Inventory inventory)
        {
            Inventory purchase = _bl.GetOneInventoryById(id);
            purchase.Quantity = purchase.Quantity - inventory.Quantity;
                HttpContext.Response.Cookies.Append("store_id", purchase.StoresId.ToString());
                HttpContext.Response.Cookies.Append("quantity", inventory.Quantity.ToString());
                HttpContext.Response.Cookies.Append("product", purchase.Product.ToString());
            if (purchase.Quantity < 0)
            {
                ModelState.AddModelError("Quantity", "You cannot buy more than we have. Please select a different quantity.");
                return View();
            }
            List<Product> productlist = _bl.ListOfProducts();
            for (int i = 0; i < productlist.Count; i++)
            {
                if (purchase.Product == productlist[i].Name)
                {
                    double total = productlist[i].Price * inventory.Quantity;
                    HttpContext.Response.Cookies.Append("plswork", total.ToString());
                    if (HttpContext.Request.Cookies["Coupon"] != null)
                    {
                        double coupon = double.Parse(HttpContext.Request.Cookies["Coupon"]);
                        total = total * coupon;
                        HttpContext.Response.Cookies.Append("total_cost", total.ToString());
                    }
                    else
                    {
                        TempData["Savings"] = 0;
                        TempData.Keep("Savings");
                        HttpContext.Response.Cookies.Append("total_cost", total.ToString());
                    }
                }
            }
            _bl.UpdateInventory(purchase);
            return RedirectToAction("Create", "OrderDetails");
        }
        [HttpGet]
        // GET: InventoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new Inventory(_bl.GetOneInventoryById(id)));
        }

        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inventory inventory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateInventory(inventory);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Index", "Inventory");
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: InventoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InventoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bl.RemoveInventory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
