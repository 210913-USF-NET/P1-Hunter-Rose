using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity = DL.Entities;
using Model = Models;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DBRepo : IRepo
    {
        private Entity.RestaurantDBContext _context;

        public DBRepo(Entity.RestaurantDBContext context)
    {
        _context = context;
    }
        public DBRepo(){}
        /// <summary>
        /// uploads customer username that user created to the database
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        public Model.Customer createCustomer(Model.Customer newCustomer)
        {
            Entity.Customer toAdd = new Entity.Customer();
            toAdd.Username = newCustomer.Name;
            _context.Customers.Add(toAdd);
            _context.SaveChanges();
            return newCustomer;
        }
        /// <summary>
        /// uploads the quantity of the product they selected when purchasing
        /// </summary>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public Model.LineItem CreateNewLineItem(Model.LineItem howMany)
        {
            Entity.LineItem toAdd = new Entity.LineItem();
            toAdd.Quantity = howMany.Quantity;
            toAdd.OrderitemsId = howMany.OrderitemsId;
            toAdd.ProductId = howMany.ProductId;
            toAdd = _context.LineItems.Add(toAdd).Entity;
            _context.SaveChanges();
            howMany.Id = toAdd.Id;
            return howMany;
        }
        /// <summary>
        /// searches customer username to make sure they're in the database
        /// </summary>
        /// <param name="searchCustomer"></param>
        /// <returns></returns>
        public Model.Customer SearchCustomer(string searchCustomer)
        {
          Entity.Customer foundCustomer = _context.Customers.FirstOrDefault(customer => customer.Username == searchCustomer); 
          if(foundCustomer != null)
            {
                return new Model.Customer {
                    Name = foundCustomer.Username,
                    Id = foundCustomer.Id
                    };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// creates a new order id when customer is purchasing items so the store can track it
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Model.OrderDetails CreateNewOrder(Models.OrderDetails order)
        {
            Entity.Orderitem toAdd = new Entity.Orderitem();
            toAdd.CustomerId = order.CustomerId;
            toAdd.StoresId = order.StoreId;
            toAdd.Date = order.Date;
            toAdd = _context.Orderitems.Add(toAdd).Entity;
            _context.SaveChanges();
            order.Id = toAdd.Id;
            return order;
        }
        /// <summary>
        /// lists store's name and location
        /// </summary>
        /// <returns></returns>
        public List <Models.Stores> StoreLocation()
        {
             return _context.Stores.Select(
                Stores => new Model.Stores() {
    
                    Name = Stores.StoreName,
                    Location = Stores.Location,
                    Id = Stores.Id
                }
            ).ToList();

        }
        /// <summary>
        /// list of customers
        /// </summary>
        /// <returns></returns>
        public List <Models.Customer> ListOfCustomers()
        {
            return _context.Customers.Select(
                Customerz => new Model.Customer(){
                    Name = Customerz.Username,
                    Id = Customerz.Id
                }
            ).ToList();
        }
        /// <summary>
        /// list of orders
        /// </summary>
        /// <returns></returns>
        public List <Models.OrderDetails> OrderHistory()
        {
            return _context.Orderitems.Where(order => order.StoresId == Models.CurrentContext.CurrentStoreId).Select(Orderhistory => new Model.OrderDetails(){
                    Id = Orderhistory.Id,
                    CustomerId = Orderhistory.CustomerId,
                    StoreId = Orderhistory.StoresId,
                    Date = Orderhistory.Date
                }
            ).ToList();
        }
        /// <summary>
        /// list of inventories in stores
        /// </summary>
        /// <returns></returns>
        public List<Models.Inventory> GetInventory()
        {
            return _context.Inventories.Where(order => order.StoresId == Models.CurrentContext.CurrentStoreId).Select(Orderhistory => new Model.Inventory(){
                    Id = Orderhistory.Id,
                    Quantity = Orderhistory.Quantity,
                    StoresId = Orderhistory.StoresId,
                    ProductId = Orderhistory.ProductId,
                    Date = Orderhistory.Date
                }
            ).ToList();
        }
        /// <summary>
        /// updates store's inventory when a purchase is made or when a manager modifies the quantity of a product
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Models.Inventory UpdateInventory(Models.Inventory quantity)
        {
            Entity.Inventory toAdd = new Entity.Inventory();
            toAdd.Id = quantity.Id;
            toAdd.Quantity = quantity.Quantity;
            toAdd.ProductId = quantity.ProductId;
            toAdd.StoresId = quantity.StoresId;
            toAdd.Date = quantity.Date;
            _context.Inventories.Update(toAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return quantity;
        }
        /// <summary>
        /// list of customer orders
        /// </summary>
        /// <returns></returns>
        public List<Models.OrderDetails> CustomerOrderHistory()
        {
             return _context.Orderitems.Where(order => order.CustomerId == Models.CurrentContext.CurrentCustomerId).Select(Orderhistory => new Model.OrderDetails(){
                    Id = Orderhistory.Id,
                    CustomerId = Orderhistory.CustomerId,
                    StoreId = Orderhistory.StoresId,
                    Date = Orderhistory.Date
                }
            ).ToList();
        }
        /// <summary>
        /// product id, quantity, and order id when a purchase is being made
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Model.LineItem CheckOutList(Models.LineItem item)
        {
            Entity.LineItem toAdd = new Entity.LineItem();
            toAdd.Quantity = item.Quantity;
            toAdd.ProductId = item.ProductId;
            toAdd.OrderitemsId = item.OrderitemsId;
            toAdd = _context.LineItems.Add(toAdd).Entity;
            _context.SaveChanges();
            item.Id = toAdd.Id;
            return item;
        }
        /// <summary>
        /// lists quantity of products
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Models.Inventory WhatsInStock(Models.Inventory quantity)
        {
            Entity.Inventory toAdd = new Entity.Inventory();
            toAdd.Quantity = quantity.Quantity;
            toAdd.ProductId = quantity.ProductId;
            toAdd.StoresId = quantity.StoresId;
            toAdd.Date = quantity.Date;
            toAdd = _context.Inventories.Add(toAdd).Entity;
            _context.SaveChanges();
            quantity.Id = toAdd.Id;
            return quantity;
        }
        /// <summary>
        /// list of products
        /// </summary>
        /// <returns></returns>

        public List<Models.Product> ListOfProducts()
        {
            return _context.Products.Select(
                Product => new Model.Product() {
    
                    Name = Product.Name,
                    Price = Product.Price
                    
                }
            ).ToList();

        }
    }
}