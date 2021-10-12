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
    
    public class OrderDetailsController : Controller
    {
        private IBL _bl;
        public OrderDetailsController(IBL bl)
        {
            _bl = bl;
        }
        public ActionResult Store()
        {
            return RedirectToAction("Index", "Store");
        }
        // GET: OrderDetailsController
        public ActionResult Index()
        {
            List<Stores> allStores = _bl.StoreLocation();
            return View(allStores);
        }
        public ActionResult OrderHistoryOfStore(OrderDetails order)
        {
            string customer = HttpContext.Request.Cookies["Customer"];
            List<Stores> store = _bl.StoreLocation();
            for (int i = 0; i < store.Count; i++)
            {
                if (order.StoreId == store[i].Id)
                {
                    string location = store[i].Location;
                    Log.Information($"{customer} is looking at the order history of store location {location}");
                }
            }
            List<OrderDetails> allOrders = _bl.OrderHistory(order);
            return View(allOrders);
        }
        public ActionResult OrderHistoryOfCustomer(OrderDetails order)
        {
            string customer = HttpContext.Request.Cookies["Customer"];
            List<Customer> history = _bl.ListOfCustomers();
            for (int i = 0; i < history.Count; i++)
            {
                if (order.CustomerId == history[i].Id)
                {
                    string Name = history[i].Name;
                    Log.Information($"{customer} is looking at the order history of Customer: {Name}");
                }
            }
            List<OrderDetails> allOrders = _bl.CustomerOrderHistory(order);
            return View(allOrders);
        }
        // GET: OrderDetailsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderDetailsController/Create

        public ActionResult Create()
        {
            OrderDetails newOrder = new OrderDetails();
            newOrder.CustomerId = int.Parse(HttpContext.Request.Cookies["user_id"]);
            newOrder.StoreId = int.Parse(HttpContext.Request.Cookies["store_id"]);
            _bl.CreateNewOrder(newOrder);
            HttpContext.Response.Cookies.Append("order_id", newOrder.Id.ToString());
            return RedirectToAction("Index", "Receipt");
        }

        // POST: OrderDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: OrderDetailsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderDetailsController/Edit/5
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

        // GET: OrderDetailsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderDetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
