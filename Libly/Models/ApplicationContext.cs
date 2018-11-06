using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Libly.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Libly.Migrations.Configuration>());
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
