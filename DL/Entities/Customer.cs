using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orderitems = new HashSet<Orderitem>();
        }

        public int Id { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Orderitem> Orderitems { get; set; }
    }
}
