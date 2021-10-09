using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        public Customer() {}

        public Customer(int id) : this()
        {
            this.Id = id;
        }

        public Customer(string name) : this()
        {
            this.Name = name;
        }

        public Customer(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }

        public Customer(Customer what)
        {
            this.Id = what.Id;
            this.Name = what.Name;
        }
        [Display(Name = "Customer Name")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "Customer name can only have alphanumeric characters, !, and ?.")]
        public string Name {get; set;}

        public int Id {get ; set ;}

        public Customer ToModel()
        {
            Customer newcust;
            try
            {
                newcust = new Customer
                {
                    Id = this.Id,
                    Name = this.Name ?? ""
                };

                //ternary 
                // IfStatement ? ifTrue : ifFalse
                // null checker
                // ifExists/notNull ?? ifFalse
                // IsNull?.Prperty
            }
            catch
            {
                throw;
            }
            return newcust;
        }

        public override string ToString()
        {
            return $"Username: {this.Name} Id: {Id}";
        }
    }
}