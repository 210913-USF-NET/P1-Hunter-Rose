using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DL
{
    public class HFMPBDBContext : DbContext
    {
        public HFMPBDBContext() : base () {}
        public HFMPBDBContext(DbContextOptions options) : base(options) {}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<OrderDetails> Orderitems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stores> Stores { get; set; }
    }
}
