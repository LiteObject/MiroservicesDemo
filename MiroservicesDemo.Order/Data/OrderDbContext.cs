namespace MiroservicesDemo.Order.Data
{
    using Microsoft.EntityFrameworkCore;
    using MiroservicesDemo.Order.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
