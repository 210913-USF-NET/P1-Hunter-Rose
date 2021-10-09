using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Stores
    {
        public Stores(){}

        public Stores(int id) : this()
        {
            this.Id = id;
        }
        public string Name {get; set;}

        public string Location {get; set;}

        public int Id {get; set;}


        public override string ToString()
        {
            return $"Store: {this.Name}, Location: {this.Location}";
        }
    }
}