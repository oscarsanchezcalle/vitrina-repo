using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Data.SeedData;
using Vitrina.Data.Entities;

namespace Vitrina.Data.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Products", schema: "dbo");
		builder.HasKey(b => b.Id);
		builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
		builder.Property(b => b.Description).HasMaxLength(2000);
		builder.Property(b => b.Price).HasPrecision(18, 2);
		builder.Property(b => b.Stock).IsRequired();
		builder.Property(b => b.Category).HasMaxLength(100);
		builder.Property(b => b.ImageUrl).HasMaxLength(500);
		builder.Property(b => b.IsActive).IsRequired();
		builder.Property(b => b.CreatedAt).IsRequired();
		builder.Property(b => b.UpdatedAt);

		builder.HasData(VitrinaSeedData.Current.Products.Select(product => new Product
		{
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			Stock = product.Stock,
			Category = product.Category,
			ImageUrl = product.ImageUrl,
			IsActive = product.IsActive,
			CreatedAt = product.CreatedAt,
			UpdatedAt = product.UpdatedAt
		}));
	}
}
