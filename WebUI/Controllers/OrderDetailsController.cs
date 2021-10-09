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
            List<OrderDetails> allOrders = _bl.OrderHistory(order);
            return View(allOrders);
        }
        public ActionResult OrderHistoryOfCustomer(OrderDetails order)
        {
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
            return View(newOrder);
        }

        // POST: OrderDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetails order)
        {
            order.CustomerId = int.Parse(HttpContext.Request.Cookies["user_id"]);
            order.StoreId = int.Parse(HttpContext.Request.Cookies["store_id"]);
            _bl.CreateNewOrder(order);
                    return RedirectToAction("Index", "Receipt");
               
       
        }

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
