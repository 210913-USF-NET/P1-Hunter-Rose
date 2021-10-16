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
    public class ReceiptController : Controller
    {
         LineItem receipt = new LineItem();
         Product totalcost = new Product();
        private IBL _bl;
            public ReceiptController(IBL bl)
            {
                _bl = bl;
            }
            // GET: ReceiptController
            public ActionResult Index()
        {
            List<LineItem> whatsincart = _bl.CheckoutList(receipt);
            foreach (LineItem item in whatsincart)
            {
                item.price = int.Parse(HttpContext.Request.Cookies["price"]);
                item.OrderitemsId = int.Parse(HttpContext.Request.Cookies["order_id"]);
                _bl.UpdateLineItem(item);
            }
            string customer = HttpContext.Request.Cookies["Customer"];
            Log.Information($"{customer} purchased {receipt.Quantity} {receipt.Product}(s) for ${receipt.total}");
            if (HttpContext.Request.Cookies["Coupon"] != null)
            {
                TempData["Savings"] = int.Parse(HttpContext.Request.Cookies["plswork"]) / 5;
                TempData.Keep("Savings");
            }
            return View(whatsincart);
        }

        public ActionResult CheckOut()
        {
            return RedirectToAction("Create", "OrderDetails");
        }
        public ActionResult CartTotal()
        {
                List<LineItem> whatsincart = _bl.CheckoutList(receipt);
                int total = 0;
                for (int i = 0; i < whatsincart.Count(); i++)
                {
                    total += whatsincart[i].Quantity * whatsincart[i].price;
                }
            if (HttpContext.Request.Cookies["Coupon"] != null)
            {
                TempData["total"] = total * int.Parse(HttpContext.Request.Cookies["Coupon"]);
            }
            TempData["total"] = total;
                TempData.Keep("total");
                return View(whatsincart);
        }
        public ActionResult Cart()
        {
            receipt.Product = HttpContext.Request.Cookies["product"];
            receipt.Quantity = int.Parse(HttpContext.Request.Cookies["quantity"]);
            //receipt.OrderitemsId = int.Parse(HttpContext.Request.Cookies["order_id"]);
            receipt.price = int.Parse(HttpContext.Request.Cookies["price"]);
            //receipt.total = int.Parse(HttpContext.Request.Cookies["total_cost"]);
            _bl.AddToCart(receipt);
            return RedirectToAction("Index", "OrderDetails");
        }
        public ActionResult Home()
        {
            return RedirectToAction("Index", "OrderDetails");
        }

        // GET: ReceiptController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReceiptController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReceiptController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReceiptController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReceiptController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
        // GET: ReceiptController/Delete/5
        // POST: ReceiptController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

                _bl.DeleteCheckOut(id);
                return RedirectToAction(nameof(CartTotal));
      
        }
    }
}
