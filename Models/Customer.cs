using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        public Customer(string name)
        {
            this.Name = name;
        }
        public Customer() {}

        public string Name {get; set;}

        public int Id {get ; set ;}

        public override string ToString()
        {
            return $"Username: {this.Name} Id: {Id}";
        }
    }
}