﻿using eShopSlution.Data.Entities;
using eShopSlution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tedu.international@gmail.com",
                NormalizedEmail = "tedu.international@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Toan",
                LastName = "Bach",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
            //modelBuilder.Entity<Slide>().HasData(
            //  new Slide() { Id = 1, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Active },
            //  new Slide() { Id = 2, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Active },
            //  new Slide() { Id = 3, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Active },
            //  new Slide() { Id = 4, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Active },
            //  new Slide() { Id = 5, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Active },
            //  new Slide() { Id = 6, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Active }
            //  );
        
    }
    }
}
