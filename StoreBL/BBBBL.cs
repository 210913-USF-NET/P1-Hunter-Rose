using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class BBBBL : IBL
    {
        private IRepo _repo;

        public List<Product> ListOfProducts() { return _repo.ListOfProducts(); }
        public Product GetOneProductById(int id) { return _repo.GetOneProductById(id); }
        public void RemoveProduct(int id) { _repo.RemoveProduct(id); }
        public Product createProduct(Product newProduct) { return _repo.createProduct(newProduct);  }
        public Customer GetOneCustomerById(int id) { return _repo.GetOneCustomerById(id); }
        public void RemoveCustomer(int id) { _repo.RemoveCustomer(id); }
        public Customer UpdateCustomer(Customer customerToUpdate) { return _repo.UpdateCustomer(customerToUpdate); }
        public List<Inventory> GetInventory() { return _repo.GetInventory(); }
        public Inventory createInventory(Inventory newInventory) { return _repo.createInventory(newInventory); }
        public Inventory GetOneInventoryById(int id) { return _repo.GetOneInventoryById(id); }
        public Inventory UpdateInventory(Inventory quantity) { return _repo.UpdateInventory(quantity); }
        public List<Customer> ListOfCustomers() { return _repo.ListOfCustomers(); }
        public List<OrderDetails> OrderHistory(OrderDetails order) { return _repo.OrderHistory(order); }
        public List<OrderDetails> CustomerOrderHistory(OrderDetails order1) { return _repo.CustomerOrderHistory(order1); }
        public List<Stores> StoreLocation() { return _repo.StoreLocation(); }
        public Stores createStore(Stores newStore) { return _repo.createStore(newStore); }
        public Stores GetOneStoreById(int id) { return _repo.GetOneStoreById(id); }
        public List<Inventory> GetInventorybyStoreId(int id) { return _repo.GetInventorybyStoreId(id); }
        public Customer createCustomer(Customer newCustomer) { return _repo.createCustomer(newCustomer); }
        public Customer SearchCustomer(Customer searchCustomer) { return _repo.SearchCustomer(searchCustomer); }
        public OrderDetails CreateNewOrder(OrderDetails order) { return _repo.CreateNewOrder(order); }
        public void RemoveInventory(int id) { _repo.RemoveInventory(id); }
        public List<LineItem> CheckoutList(LineItem item) { return _repo.CheckoutList(item); }
        public LineItem GetOneLineitemById(int id) { return _repo.GetOneLineitemById(id); }
        public LineItem AddToCart(LineItem item) { return _repo.AddToCart(item); }
        public void DeleteCheckOut(int id) { _repo.DeleteCheckOut(id); }
        public LineItem UpdateLineItem(LineItem item) { return _repo.UpdateLineItem(item); }
        public BBBBL(IRepo irepo){
             _repo = irepo;
            
        } 
        public List<Stores> SearchStore(string quertStr){
            return new List<Stores>();
        }
    }
}