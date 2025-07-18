﻿

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			//builder.ToTable("Products", OrderingContext.DEFAULT_SCHEMA);
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
				.HasConversion(
					productId => productId.Value,
					dbId => ProductId.Of(dbId));

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.Price)
				.IsRequired();
		}
	}

}
