using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Data.SeedData;
using Vitrina.Data.Entities;

namespace Vitrina.Data.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users", schema: "dbo");
		builder.HasKey(b => b.Id);
		builder.Property(b => b.Name).IsRequired().HasMaxLength(150);
		builder.Property(b => b.Email).IsRequired().HasMaxLength(255);
		builder.HasIndex(b => b.Email).IsUnique();
		builder.Property(b => b.Username).IsRequired().HasMaxLength(100);
		builder.HasIndex(b => b.Username).IsUnique();
		builder.Property(b => b.PasswordHash).IsRequired().HasMaxLength(500);
		builder.Property(b => b.Role).IsRequired().HasConversion<byte>().HasColumnType("tinyint");
		builder.Property(b => b.CreatedAt).IsRequired();

		builder.HasData(VitrinaSeedData.Current.Users.Select(user => new User
		{
			Id = user.Id,
			Name = user.Name,
			Email = user.Email,
			Username = user.Username,
			PasswordHash = user.PasswordHash,
			Role = user.Role,
			CreatedAt = user.CreatedAt
		}));
	}
}
