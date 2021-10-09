using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Inventory
    {
        public Inventory(){}

        public Inventory(Inventory what)
        {
            this.Id = what.Id;
            this.Quantity = what.Quantity;
            this.StoresId = what.StoresId;
            this.Product = what.Product;
            this.Date = what.Date;
        }

        public Inventory(int id) : this()
        {
            this.Id = id;
        }

        public Inventory(int stock, int storesId, string productName, DateTime whatday)
        {
            this.Quantity = stock;
            this.StoresId = storesId;
            this.Product = productName;
            this.Date = whatday;
        }
        public int Id {get; set;}

        public int Quantity {get; set;}

        public int StoresId {get; set;}

        public string Product {get; set;}

        public DateTime Date {get ; set;}

        public override string ToString()
        {
            return $" Date: {Date} Store: {StoresId} ProductId: {Product} has {Quantity} left in stock. Quantity check {Id}.";
        }
    }
}