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

        Customer SearchCustomer(string searchCustomer);

        OrderDetails CreateNewOrder(OrderDetails order);

        List <Models.Stores> StoreLocation();

        List<Models.Product> ListOfProducts();
    }
}