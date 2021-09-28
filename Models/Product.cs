using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Models
{
    public class Product
    {
        public Product(){}

        public string Name {get; set;}

        public int? Price {get; set;}

        public int? Id {get; set;}

        public List<Product> Products { get; set; }

         public override string ToString()
        {
            return $"Product: {this.Name}, Price: ${this.Price}";
        }
    }

}