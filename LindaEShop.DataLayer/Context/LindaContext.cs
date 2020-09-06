using LindaEShop.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.DataLayer.Context
{
	public class LindaContext : DbContext
	{
		public LindaContext(DbContextOptions<LindaContext> options) : base(options)
		{

		}

		#region User
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }

		#endregion

		#region Product
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductGroup> ProductGroups { get; set; }
		public DbSet<Size> Sizes { get; set; }
		public DbSet<SizeToProduct> SizeToProducts { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<ColorToProduct> ColorToProducts { get; set; }
		public DbSet<ProductGallery> productGalleries { get; set; }
		#endregion

		#region Order

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		#endregion


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasQueryFilter(r => !r.IsDelete);
		}

	}
}
