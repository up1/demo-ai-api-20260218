using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLineItem> OrderLineItems => Set<OrderLineItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id")
                  .UseIdentityAlwaysColumn();
            entity.Property(e => e.UniqueId).HasColumnName("unique_id")
                  .IsRequired();
            entity.HasIndex(e => e.UniqueId).IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id")
                  .UseIdentityAlwaysColumn();
            entity.Property(e => e.UniqueId).HasColumnName("unique_id")
                  .IsRequired();
            entity.HasIndex(e => e.UniqueId).IsUnique();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id")
                  .UseIdentityAlwaysColumn();
            entity.Property(e => e.CustomerId).HasColumnName("customer_id")
                  .IsRequired();
            entity.Property(e => e.OrderDate).HasColumnName("order_date")
                  .IsRequired();
            entity.Property(e => e.ShippingStreet).HasColumnName("shipping_street")
                  .IsRequired();
            entity.Property(e => e.ShippingCity).HasColumnName("shipping_city")
                  .IsRequired();
            entity.Property(e => e.ShippingState).HasColumnName("shipping_state")
                  .IsRequired();
            entity.Property(e => e.ShippingZipCode).HasColumnName("shipping_zip_code")
                  .IsRequired();
            entity.Property(e => e.BillingStreet).HasColumnName("billing_street")
                  .IsRequired();
            entity.Property(e => e.BillingCity).HasColumnName("billing_city")
                  .IsRequired();
            entity.Property(e => e.BillingState).HasColumnName("billing_state")
                  .IsRequired();
            entity.Property(e => e.BillingZipCode).HasColumnName("billing_zip_code")
                  .IsRequired();
            entity.Property(e => e.Status).HasColumnName("status")
                  .IsRequired();

            entity.HasOne(e => e.Customer)
                  .WithMany()
                  .HasForeignKey(e => e.CustomerId);

            entity.HasMany(e => e.LineItems)
                  .WithOne(e => e.Order)
                  .HasForeignKey(e => e.OrderId);
        });

        modelBuilder.Entity<OrderLineItem>(entity =>
        {
            entity.ToTable("order_line_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id")
                  .UseIdentityAlwaysColumn();
            entity.Property(e => e.OrderId).HasColumnName("order_id")
                  .IsRequired();
            entity.Property(e => e.ProductId).HasColumnName("product_id")
                  .IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("quantity")
                  .IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price")
                  .HasColumnType("numeric(10,2)")
                  .IsRequired();

            entity.HasOne(e => e.Product)
                  .WithMany()
                  .HasForeignKey(e => e.ProductId);
        });
    }
}
