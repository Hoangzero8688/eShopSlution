using eShopSlution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSlution.Data.Configruration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(X =>X.Id).UseIdentityColumn();
            builder.Property(X =>X.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(X =>X.Caption).HasMaxLength(200);

            builder.HasOne(x => x.product).WithMany(x =>x.ProductImages).HasForeignKey(x => x.ProductId);
        }
    }
}
