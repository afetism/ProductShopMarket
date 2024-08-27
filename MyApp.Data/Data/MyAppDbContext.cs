using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

using MyApp.Data.Models.EntityModel;
using System.Data;

namespace MyApp.Data.Data;

public class MyAppDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseLazyLoadingProxies()
						 .UseSqlServer("Server=DESKTOP-0P1DC60\\SQLEXPRESS;Initial Catalog=AfetProductManagment;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");




	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Firstname).IsRequired();
			entity.Property(e => e.Lastname).IsRequired();
			entity.Property(e => e.Email).IsRequired();
			entity.Property(e => e.Password).IsRequired();
			entity.Property(e => e.DateofBirth).IsRequired();

			entity.HasMany(e => e.History)
				  .WithOne(e => e.User)
				  .HasForeignKey(e => e.UserId);

			entity.HasOne(e => e.Cart)
				  .WithOne(e => e.User)
				  .HasForeignKey<Cart>(e => e.UserId);

			entity.HasOne(e => e.LikedItem)
				  .WithOne(e => e.User)
				  .HasForeignKey<LikedItem>(e => e.UserId);

			entity.HasOne(e => e.PhotoUs)
				  .WithOne(e => e.User)
				  .HasForeignKey<PhotoUser>(e => e.UserId);

			entity.HasMany(e => e.CreditCards)
				  .WithOne(e => e.User)
				  .HasForeignKey(e => e.UserId);
		});

		modelBuilder.Entity<Admin>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.FirstName).IsRequired();
			entity.Property(e => e.LastName).IsRequired();
			entity.Property(e => e.Email).IsRequired();
			entity.Property(e => e.Password).IsRequired();
		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Date).IsRequired();

			entity.HasMany(e => e.OrderItems)
				  .WithOne(e => e.Order)
				  .HasForeignKey(e => e.OrderId);
		});

		modelBuilder.Entity<OrderItem>(entity =>
		{
			entity.HasKey(e => new { e.OrderId, e.ProductId });

			entity.HasOne(e => e.Product)
				  .WithMany(e => e.OrderItems)
				  .HasForeignKey(e => e.ProductId);
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired();
			entity.Property(e => e.Price).IsRequired();
			entity.Property(e => e.Quantity).IsRequired();

			entity.HasOne(e => e.Category)
				  .WithMany(e => e.Products)
				  .HasForeignKey(e => e.CategoryId);

			entity.HasOne(e => e.Photo)
				  .WithOne(e => e.Product)
				  .HasForeignKey<PhotoProduct>(e => e.ProductId);
		});

		modelBuilder.Entity<Category>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired();
		});

		modelBuilder.Entity<CreditCard>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Number).IsRequired();
			entity.Property(e => e.ExpirationDate).IsRequired();
			entity.Property(e => e.CVV).IsRequired();
		});

		modelBuilder.Entity<Cart>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		modelBuilder.Entity<LikedItem>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		modelBuilder.Entity<PhotoUser>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Description).IsRequired();
			entity.Property(e => e.FileExtension).IsRequired();
			entity.Property(e => e.Size).IsRequired();
			entity.Property(e => e.Bytes).IsRequired();
		});

		modelBuilder.Entity<PhotoProduct>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Description).IsRequired();
			entity.Property(e => e.FileExtension).IsRequired();
			entity.Property(e => e.Size).IsRequired();
			entity.Property(e => e.Bytes).IsRequired();
		});
	}

	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<LikedItem> LikedItems { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Admin> Admins { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<CreditCard> CreditCarts { get; set; }
	public DbSet<PhotoProduct> PhotoProducts { get; set; }
	public DbSet<PhotoUser> PhotoUsers { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }


}