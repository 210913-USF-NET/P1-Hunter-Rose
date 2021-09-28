using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Store
    {
        public Store()
        {
            Inventories = new HashSet<Inventory>();
            Orderitems = new HashSet<Orderitem>();
        }

        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Orderitem> Orderitems { get; set; }
    }
}
