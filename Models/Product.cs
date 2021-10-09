using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Models
{
    public class Product
    {
        public Product() { }
       
        public Product(int id)
        {
            this.Id = id;
        }
        public Product(Product what)
        {
            this.Id = what.Id;
            this.Name = what.Name;
            this.Price = what.Price;
        }
        public Product(string name, int price, int id)
        {
            this.Name = name;
            this.Price = price;
            this.Id = id;
        }

        public string Name {get; set;}

        public int Price {get; set;}

        public int Id {get; set;}

         public override string ToString()
        {
            return $"Product: {this.Name}, Price: ${this.Price}";
        }
    }

}