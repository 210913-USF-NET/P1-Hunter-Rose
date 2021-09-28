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
        public Model.Customer createCustomer(Model.Customer newCustomer)
        {
            Entity.Customer toAdd = new Entity.Customer();
            toAdd.Username = newCustomer.Name;
            _context.Customers.Add(toAdd);
            _context.SaveChanges();
            return newCustomer;
        }
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
        public List <Models.Customer> ListOfCustomers()
        {
            return _context.Customers.Select(
                Customerz => new Model.Customer(){
                    Name = Customerz.Username,
                    Id = Customerz.Id
                }
            ).ToList();
        }
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