namespace CarDealer.Data
{
    using Models;
    using System.Data.Entity;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
            : base("name=CarDealerContext")
        {
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Parts)
                .WithRequired(p => p.Supplier)
                .WillCascadeOnDelete(true);
        }
    }
}