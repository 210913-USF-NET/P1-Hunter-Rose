using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public interface IBL
    {
        List<Stores> SearchStore(string quertStr);
        List<OrderDetails> OrderHistory(OrderDetails order);
        List<OrderDetails> CustomerOrderHistory(OrderDetails order1);
        List<Customer> ListOfCustomers();
        Customer createCustomer(Customer newCustomer);
        Customer SearchCustomer(Customer searchCustomer);
        Inventory createInventory(Inventory newInventory);
        List<Stores> StoreLocation();
        Stores createStore(Stores newStore);
        Stores GetOneStoreById(int id);
        List<Inventory> GetInventory();
        List<Inventory> GetInventorybyStoreId(int id);
        Inventory GetOneInventoryById(int id);
        Inventory UpdateInventory(Inventory quantity);
        List<Product> ListOfProducts();
        Product createProduct(Product newProduct);
        Product GetOneProductById(int id);
        public void RemoveProduct(int id);
        Customer GetOneCustomerById(int id);
        Customer UpdateCustomer(Customer customerToUpdate);
        public void RemoveCustomer(int id);
        OrderDetails CreateNewOrder(OrderDetails order);
        public void RemoveInventory(int id);
        LineItem CheckOutList(LineItem item);
        List<LineItem> GetOneLineitemById(int id);
    }
}