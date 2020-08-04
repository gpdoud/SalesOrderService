using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SalesOrderService.Models;

namespace SalesOrderService.Data {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Orderitem> Orderitems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Item>(e => {
                e.HasIndex(x => x.UPC).IsUnique();
            });
        }
    }
}
