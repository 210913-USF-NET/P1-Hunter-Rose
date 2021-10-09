using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;
using DL;

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
                    _bl.UpdateInventory(purchase);
                    List<Product> productlist = _bl.ListOfProducts();
                    for (int i = 0; i < productlist.Count; i++)
                    {
                        if (purchase.Product == productlist[i].Name)
                        {
                            int total = productlist[i].Price * inventory.Quantity;
                            HttpContext.Response.Cookies.Append("total_cost", total.ToString());
                        }
                    }
                    return RedirectToAction("Create", "OrderDetails");
        }

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
                    return RedirectToAction(nameof(StoreIndex));
                }
                return RedirectToAction(nameof(Edit));
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
