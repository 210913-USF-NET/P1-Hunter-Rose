using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Orderitem
    {
        public Orderitem()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? CustomerId { get; set; }
        public int? StoresId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Stores { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
