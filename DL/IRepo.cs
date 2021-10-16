using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace DL
{
    public interface IRepo
    {
        Customer createCustomer(Customer newCustomer);
        LineItem CreateNewLineItem(LineItem howMany);
        Customer SearchCustomer(Customer searchCustomer);
        List<Customer> ListOfCustomers();
        OrderDetails CreateNewOrder(OrderDetails order);
        Inventory createInventory(Inventory newInventory);
        List<Inventory> GetInventorybyStoreId(int id);
        Inventory GetOneInventoryById(int id);
        Inventory UpdateInventory(Inventory quantity);
        List <Models.Stores> StoreLocation();
        Stores createStore(Stores newStore);
        Stores GetOneStoreById(int id);
        List<Models.Product> ListOfProducts();
        Product GetOneProductById(int id);
        public void RemoveProduct(int id);
        Product createProduct(Product newProduct);
        List<OrderDetails> OrderHistory(OrderDetails order);
        List<OrderDetails> CustomerOrderHistory(OrderDetails order1);
        List<Inventory> GetInventory();
        Customer GetOneCustomerById(int id);
        Customer UpdateCustomer(Customer customerToUpdate);
        void RemoveCustomer(int id);
        LineItem AddToCart(LineItem item);
        public List<LineItem> CheckoutList(LineItem item);
        LineItem GetOneLineitemById(int id);
        public void RemoveInventory(int id);
        void DeleteCheckOut(int id);
        public LineItem UpdateLineItem(LineItem item);
    }
}