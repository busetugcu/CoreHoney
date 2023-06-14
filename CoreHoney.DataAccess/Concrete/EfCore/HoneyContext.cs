using CoreHoney.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.DataAccess.Concrete.EfCore
{
    public class HoneyContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-7F6KSNV; Database=CoreHoney; Integrated Security=true;TrustServerCertificate=True ");
        }

        public DbSet<Honey> Honeys { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
