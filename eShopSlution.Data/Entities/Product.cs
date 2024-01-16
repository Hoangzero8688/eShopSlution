using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSlution.Data.Entities;

namespace eShopSlution.Data.Entities
{
    //[Table("Products")]
    public class Product
    {
        public int Id { get; set; } 
        public decimal Price { get; set; } 
        public decimal OriginalPrice { get; set; } 
        public int Stock { get; set; } 
        public int ViewCount { get; set; } 
        public string SeoAlias { get; set; }
        public DateTime DataCreated { get; set; }
        //[Required]
        public IEnumerable<Cart> Carts { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { set; get; }
        public IEnumerable<ProductInCategory> ProductInCategories { set; get; }
        public IEnumerable<ProductTranslation> ProductTranslations { set; get; }
        public IEnumerable<ProductImage> ProductImages { set; get; }
    }
}
