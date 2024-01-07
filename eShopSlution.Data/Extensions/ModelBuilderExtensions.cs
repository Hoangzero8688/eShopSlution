using eShopSlution.Data.Entities;
using eShopSlution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSlution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig { Key = "HomeTitle", Value = "This is home page of eShopSlution" },
                new AppConfig { Key = "HomeKeyword", Value = "This is homeKeyword page of eShopSlution" },
                 new AppConfig { Key = "HomeDescription", Value = "This is description page of eShopSlution" }
               );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "Vi-VN", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "En-Us", Name = "English", IsDefault = false }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOder = 1,
                    Status = Status.Active,

                }
               ,

                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOder = 2,
                    Status = Status.Active,


                }
                );

            modelBuilder.Entity<CategoryTranslation>().HasData(
                  new List<CategoryTranslation>() {
                    new CategoryTranslation() { Id = 1, CategoryId =1, Name = "Áo nam", LanguageId = "Vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Sản phẩm áo thời trang nam" },
                     new CategoryTranslation() {Id = 2, CategoryId =1, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "Men-SHirt", SeoDescription = "The shirt products for men", SeoTitle = "The shirt products for men" },
                   new CategoryTranslation() {Id = 3,CategoryId =2, Name = "Áo nữ", LanguageId = "Vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Sản phẩm áo thời trang nam" },
                         new CategoryTranslation() {Id = 4,CategoryId =2, Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "Men-SHirt", SeoDescription = "The shirt products for men", SeoTitle = "The shirt products for men" }
                  });


            modelBuilder.Entity<Product>().HasData(
             new Product()
             {
                 Id = 1,
                 DataCreated = DateTime.Now,
                 OriginalPrice = 100000,
                 Price = 200000,
                 Stock = 0,
                 ViewCount = 0,

                 SeoAlias = "test"
                
             }
             );

            modelBuilder.Entity<ProductTranslation>().HasData(

                    new ProductTranslation()
                    {

                        Id = 1,
                        ProductId = 1,
                        Name = "Áo sơ mi trắng Việt Tiến",
                        LanguageId = "Vi-VN",
                        SeoAlias = "ao-so-mi-trang-Viet-Tien",
                        SeoDescription = "Sản phẩm Áo sơ mi trắng Việt Tiến",
                        SeoTitle = "Sản phẩm Áo sơ mi trắng Việt Tiến",
                        Details = "Áo sơ mi trắng Việt Tiến",
                        Description = "Áo sơ mi trắng Việt Tiến"
                    },
                     new ProductTranslation()
                     {
                         Id = 2,
                         ProductId = 1,
                         Name = "Viet Tien Men T-Shirt",
                         LanguageId = "en-US",
                         SeoAlias = "Viet-Tien-men-shirt",
                         SeoDescription = "Viet Tien Men T-Shirt",
                         SeoTitle = "Viet Tien Men T-Shirt",
                         Details = "Viet Tien Men T-Shirt",
                         Description = "Viet Tien Men T-Shirt"
                     }
                     );

            modelBuilder.Entity<ProductInCategory>().HasData(

                       new ProductInCategory()
                       {
                           CategoryId = 1,
                           ProductId = 1, 
                       }
                       );
        }
    }
}
