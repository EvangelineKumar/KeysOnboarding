using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace KeysOnboarding.Models
{
    public class KeysContext : DbContext
    {
        public KeysContext() : base("KeysContext")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductSold> ProductSolds { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}