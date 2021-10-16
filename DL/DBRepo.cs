using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entity = DL.Entities;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace DL
{
    public class DBRepo : IRepo
    {
        private HFMPBDBContext _context;

        public DBRepo(HFMPBDBContext context)
    {
        _context = context;
    }
        public DBRepo(){}
        /// <summary>
        /// uploads customer username that user created to the database
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        public Customer createCustomer(Customer newCustomer)
        {
            newCustomer = _context.Add(newCustomer).Entity;
            _context.SaveChanges();
            return newCustomer;
        }
        public void RemoveCustomer(int id)
        {
            _context.Customers.Remove(GetOneCustomerById(id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
        /// <summary>
        /// uploads the quantity of the product they selected when purchasing
        /// </summary>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public LineItem CreateNewLineItem(LineItem howMany)
        {
            LineItem toAdd = new LineItem();
            toAdd.Quantity = howMany.Quantity;
            toAdd.OrderitemsId = howMany.OrderitemsId;
            toAdd.Product = howMany.Product;
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
        public Customer SearchCustomer(Customer customer)
        {
            List<Customer> customerList = ListOfCustomers();
            for (int i = 0; i < customerList.Count; i++)
            {
                if (customer.Name == customerList[i].Name)
                {
                    return new Customer
                    {
                        Name = customerList[i].Name,
                        Id = customerList[i].Id
                    };
                }
            }
            return null;
        }

        //public List<Cart> GetCheckOutList()
        //{
        //    return _context.Cart
        //        .Select(
        //        r => new Cart()
        //        { 
        //            Id = r.Id,
        //            Item = r.Item,
        //            Price = r.Price,
        //            Quantity = r.Quantity,
        //            Size = r.Size,
        //            TotalPrice = r.TotalPrice,
        //            CustomerId = r.CustomerId,
        //            StoreId = r.StoreId
        //        }).ToList();
        //}
        /// <summary>
        /// creates a new order id when customer is purchasing items so the store can track it
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderDetails CreateNewOrder(OrderDetails order)
        {
            OrderDetails toAdd = new OrderDetails();
            toAdd.CustomerId = order.CustomerId;
            toAdd.StoreId = order.StoreId;
            toAdd.Date = DateTime.Now;
            _context.Orderitems.Add(toAdd);
            _context.SaveChanges();
            order.Id = toAdd.Id;
            return order;
        }
        /// <summary>
        /// lists store's name and location
        /// </summary>
        /// <returns></returns>
        public List <Stores> StoreLocation()
        {
             return _context.Stores.Select(
                Stores => new Stores() {
    
                    Name = Stores.Name,
                    Location = Stores.Location,
                    Id = Stores.Id
                }
            ).ToList();

        }
        public Stores GetOneStoreById(int id)
        {
            return _context.Stores
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }
        public Stores createStore(Stores newStore)
        {
            newStore = _context.Add(newStore).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return newStore;
        }
        /// <summary>
        /// list of customers
        /// </summary>
        /// <returns></returns>
        public List <Customer> ListOfCustomers()
        {
            return _context.Customers.Select(
                Customerz => new Customer(){
                    Name = Customerz.Name,
                    Id = Customerz.Id
                }
            ).ToList();
        }
        /// <summary>
        /// list of orders
        /// </summary>
        /// <returns></returns>
        public Customer UpdateCustomer(Customer customerToUpdate)
        {
            Customer custToUpdate = new Customer()
            {
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name

            };
            custToUpdate = _context.Customers.Update(custToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Customer()
            {
                Id = custToUpdate.Id,
                Name = custToUpdate.Name

            };
        }

        public List<OrderDetails> OrderHistory(OrderDetails order1)
        {
            return _context.Orderitems.Where(order => order.StoreId == order1.StoreId).Select(Orderhistory => new OrderDetails()
            {
                Id = Orderhistory.Id,
                CustomerId = Orderhistory.CustomerId,
                StoreId = Orderhistory.StoreId,
                Date = Orderhistory.Date
            }
            ).ToList();
        }
        public Inventory createInventory(Inventory newInventory)
        {
            Inventory toAdd = new Inventory();
            toAdd.Id = newInventory.Id;
            toAdd.Quantity = newInventory.Quantity;
            toAdd.StoresId = newInventory.StoresId;
            toAdd.Product = newInventory.Product;
            toAdd.Date = DateTime.Now;
            _context.Inventories.Add(toAdd);
            _context.SaveChanges();
            return newInventory;
        }
        /// <summary>
        /// list of inventories in stores
        /// </summary>
        /// <returns></returns>

        public List<Inventory> GetInventory()
        {
            return _context.Inventories.Select(
                inventories => new Inventory()
            {
                Id = inventories.Id,
                Quantity = inventories.Quantity,
                StoresId = inventories.StoresId,
                Product = inventories.Product,
                Date = inventories.Date
            }
            ).ToList();
        }
        public Inventory GetOneInventoryById(int id)
        {
            return _context.Inventories
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }
        public List<Inventory> GetInventorybyStoreId(int id)
        {
            return _context.Inventories.Where(thisStore => thisStore.StoresId == id).Select(
                inventories => new Inventory()
                {
                    Id = inventories.Id,
                    Quantity = inventories.Quantity,
                    StoresId = inventories.StoresId,
                    Product = inventories.Product,
                    Date = inventories.Date
                }
            ).ToList();
        }
        /// <summary>
        /// updates store's inventory when a purchase is made or when a manager modifies the quantity of a product
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Inventory UpdateInventory(Inventory quantity)
        {
            Inventory toAdd = new Inventory();
            toAdd.Id = quantity.Id;
            toAdd.Quantity = quantity.Quantity;
            toAdd.Product = quantity.Product;
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
        public List<OrderDetails> CustomerOrderHistory(OrderDetails order1)
        {
            return _context.Orderitems.Where(order => order.CustomerId == order1.CustomerId).Select(Orderhistory => new OrderDetails()
            {
                Id = Orderhistory.Id,
                CustomerId = Orderhistory.CustomerId,
                StoreId = Orderhistory.StoreId,
                Date = Orderhistory.Date
            }
           ).ToList();
        }
        /// <summary>
        /// product id, quantity, and order id when a purchase is being made
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public LineItem AddToCart(LineItem item)
        {
            LineItem toAdd = new LineItem();
            toAdd.Quantity = item.Quantity;
            toAdd.Product = item.Product;
            //toAdd.OrderitemsId = item.OrderitemsId;
            toAdd.total = item.total;
            toAdd.price = item.price;
            toAdd = _context.LineItems.Add(toAdd).Entity;
            _context.SaveChanges();
            item.Id = toAdd.Id;
            return item;
        }
        public void DeleteCheckOut(int id)
        {
            _context.LineItems.Remove(GetOneLineitemById(id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }


        public List<LineItem> CheckoutList(LineItem lineitem)
        {
            return _context.LineItems.Where(order => order.OrderitemsId == lineitem.OrderitemsId).Select(
                cart => new LineItem()
                {
                    Id = cart.Id,
                    Quantity = cart.Quantity,
                    price = cart.price,
                    Product = cart.Product
                }
            ).ToList();
        }

        public LineItem UpdateLineItem(LineItem item)
        {
            LineItem toAdd = new LineItem();
            toAdd.Id = item.Id;
            toAdd.Quantity = item.Quantity;
            toAdd.Product = item.Product;
            toAdd.OrderitemsId = item.OrderitemsId;
            toAdd.price = item.price;
            _context.LineItems.Update(toAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return item;
        }
        public LineItem GetOneLineitemById(int id)
        {
            return _context.LineItems
                 .AsNoTracking()
                 .FirstOrDefault(r => r.Id == id);
        }
        /// <summary>
        /// lists quantity of products
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Inventory WhatsInStock(Inventory quantity)
        {
            Inventory toAdd = new Inventory();
            toAdd.Quantity = quantity.Quantity;
            toAdd.Product = quantity.Product;
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
        public Customer GetOneCustomerById(int id)
        {
            return _context.Customers
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }
        public List<Product> ListOfProducts()
        {
            return _context.Products.Select(
                Product => new Product() {
    
                    Name = Product.Name,
                    Price = Product.Price,
                    Id = Product.Id
                }
            ).ToList();

        }
        public Product createProduct(Product newProduct)
        {
            newProduct = _context.Add(newProduct).Entity;
            _context.SaveChanges();
            return newProduct;
        }
        public Product GetOneProductById(int id)
        {
            return _context.Products
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }

        public void RemoveProduct(int id)
        {
            _context.Products.Remove(GetOneProductById(id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
        public void RemoveInventory(int id)
        {
            _context.Inventories.Remove(GetOneInventoryById(id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
    }
}