using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class LineItem
    {
        public LineItem(){}

        public int Id {get; set;}

        public int? Quantity {get; set;}

        public int? ProductId {get; set;}

        public int? OrderitemsId {get; set;}

         public override string ToString()
        {
            return $"Order Number: {OrderitemsId}, Product: {this.ProductId}, Quantity: {this.Quantity}";
        }
    }
}