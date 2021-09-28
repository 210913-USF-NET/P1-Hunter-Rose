using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Inventory
    {
        public Inventory(){}

        public Inventory(int stock, int storesId, int productId, DateTime whatday)
        {
            this.Quantity = stock;
            this.StoresId = storesId;
            this.ProductId = productId;
            this.Date = whatday;
        }
        public int Id {get; set;}

        public int? Quantity {get; set;}

        public int? StoresId {get; set;}

        public int? ProductId {get; set;}

        public DateTime? Date {get ; set;}

        public override string ToString()
        {
            return $" Date: {Date} Store: {StoresId} ProductId: {ProductId} has {Quantity} left in stock. Quantity check {Id}.";
        }
    }
}