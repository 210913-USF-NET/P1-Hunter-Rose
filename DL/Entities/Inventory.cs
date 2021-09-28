using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Date { get; set; }
        public int? StoresId { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Stores { get; set; }
    }
}
