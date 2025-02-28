using TestApp2._0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestApp2._0.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set a Partition Key for better performance
        modelBuilder.Entity<Customer>().ToContainer("Customers").HasPartitionKey(c => c.CustomerId);
        modelBuilder.Entity<Address>().ToContainer("Addresses").HasPartitionKey(c => c.AddressId);
        modelBuilder.Entity<Driver>().ToContainer("Drivers").HasPartitionKey(c => c.DriverId);
        modelBuilder.Entity<Product>().ToContainer("Products").HasPartitionKey(c => c.ProductId);
        
        modelBuilder.Entity<Transportation>().ToContainer("Transportations").HasPartitionKey(t => t.TransportationId);
        modelBuilder.Entity<Delivery>().ToContainer("Deliveries").HasPartitionKey(d => d.DeliveryId);
        modelBuilder.Entity<DeliveryItem>().ToContainer("DeliveryItems").HasPartitionKey(di => di.id);
    
        // Ensure unique IDs
        modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
        modelBuilder.Entity<Address>().HasKey(c => c.AddressId);
        modelBuilder.Entity<Driver>().HasKey(c => c.DriverId);
        modelBuilder.Entity<Product>().HasKey(c => c.ProductId);
        modelBuilder.Entity<DeliveryItem>().HasKey(d => d.id);
        modelBuilder.Entity<Delivery>().HasKey(d => d.DeliveryId);
        modelBuilder.Entity<Stop>().HasKey(s => s.StopId);
        modelBuilder.Entity<Transportation>().HasKey(t => t.TransportationId);

        base.OnModelCreating(modelBuilder);
    }



    public DbSet<Transportation> Transportations { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<DeliveryItem> DeliveryItems { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<Stop> Stops { get; set; }

    public DbSet<Address> Addresses { get; set; }
    
    
    
    
    
    
    
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<DeliveryItem>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.TotalCost = entry.Entity.SalesUnitPrice * entry.Entity.OrderedCount;
            }
        }
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<DeliveryItem>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.TotalCost = entry.Entity.SalesUnitPrice * entry.Entity.OrderedCount;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
    
}