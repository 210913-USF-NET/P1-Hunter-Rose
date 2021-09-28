using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class LineItem
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int? ProductId { get; set; }
        public int? OrderitemsId { get; set; }

        public virtual Orderitem Orderitems { get; set; }
        public virtual Product Product { get; set; }
    }
}
