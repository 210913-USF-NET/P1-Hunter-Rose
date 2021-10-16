using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public int StoreId { get; set; }

        public override string ToString()
        {
            return $"{this.Item} Price: {this.Price.ToString("C")} Size: {this.Size} Quantity: {this.Quantity}";
        }
    }
}