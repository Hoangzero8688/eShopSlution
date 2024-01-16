//using eShopSlution.Application.Catalog.Products;
//using eShopSlution.Data.EF;
//using eShopSlution.Data.Entities;
//using eShopSolution.Utilities.Exceptions;
//using eShopSolution.ViewModels.Catalog;
//using eShopSolution.ViewModels.Catalog.Products.Manager;
//using eShopSolution.ViewModels.Common;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace eShopSlution.Application.Catalog
//{
//    public class ManageProductService : ImanagerProductService
//    {
//        private readonly eShopDbContext _context;
//        //private readonly IStorageService _storageService;
//        //private const string USER_CONTENT_FOLDER_NAME = "user-content";

//        public ManageProductService(eShopDbContext context/*, IStorageService storageService*/)
//        {
//            _context = context;
//            //_storageService = storageService;
//        }


//        public async Task AddViewCount(int ProductId)
//        {
//            var product = await _context.Products.FindAsync(ProductId);
//            product.ViewCount += 1;
//            await _context.SaveChangesAsync();
//        }

//        public  async Task<int> Create(ProductCreateRequest request)
//        { 
//            var languages = _context.Languages;
//            var translations = new List<ProductTranslation>();
//            foreach (var language in languages)
//            {
//                if (language.Id == request.languageId)
//                {
//                    translations.Add(new ProductTranslation()
//                    {
//                        Name = request.Name,
//                        Description = request.Description,
//                        Details = request.Details,
//                        SeoDescription = request.SeoDescription,
//                        SeoAlias = request.SeoAlias,
//                        SeoTitle = request.SeoTitle,
//                        LanguageId = request.languageId
//                    });
//                }
//                else
//                {
//                    //translations.Add(new ProductTranslation()
//                    //{
//                    //    Name = SystemConstants.ProductConstants.NA,
//                    //    Description = SystemConstants.ProductConstants.NA,
//                    //    SeoAlias = SystemConstants.ProductConstants.NA,
//                    //    LanguageId = language.Id
//                    //});
//                }
//            }
//            var product = new Product()
//            {
//                Price = request.Price,
//                OriginalPrice = request.OriginalPrice,
//                Stock = request.Stock,
//                ViewCount = 0,
//                DataCreated = DateTime.Now,
//                ProductTranslations = translations
//            };
//            ////Save image
//            //if (request.ThumbnailImage != null)
//            //{
//            //    product.ProductImages = new List<ProductImage>()
//            //    {
//            //        new ProductImage()
//            //        {
//            //            Caption = "Thumbnail image",
//            //            DateCreated = DateTime.Now,
//            //            FileSize = request.ThumbnailImage.Length,
//            //            ImagePath = await this.SaveFile(request.ThumbnailImage),
//            //            IsDefault = true,
//            //            SortOrder = 1
//            //        }
//            //    };
//            //}
//            _context.Products.Add(product);
//            await _context.SaveChangesAsync();
//            return product.Id;
//        }

       

//        public async Task<int> Delete(int ProductId)
//        { 
//            var product = await _context.Products.FindAsync(ProductId);
//            if (product == null) throw new EShopException($"Cannot find a product: {ProductId}");

//            //var images = _context.ProductImages.Where(i => i.ProductId == ProductId);
//            //foreach (var image in images)
//            //{
//            //    await _storageService.DeleteFileAsync(image.ImagePath);
//            //}

//            _context.Products.Remove(product);

//            return await _context.SaveChangesAsync();
//        }

//        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
//        {
//            //1. Select join
//            var query = from p in _context.Products
//                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
//                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
//                        from pic in ppic.DefaultIfEmpty()
//                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
//                        from c in picc.DefaultIfEmpty()
//                        //join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
//                        from pi in ppi.DefaultIfEmpty()
//                        where pt.LanguageId == request.LanguageId && pi.IsDefault == true
//                        select new { p, pt, pic, pi };
//            //2. filter
//            if (!string.IsNullOrEmpty(request.Keyword))
//                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

//            if (request.CategoryId != null && request.CategoryId != 0)
//            {
//                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
//            }

//            //3. Paging
//            int totalRow = await query.CountAsync();

//            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
//                .Take(request.PageSize)
//                .Select(x => new ProductVm()
//                {
//                    Id = x.p.Id,
//                    Name = x.pt.Name,
//                    DateCreated = x.p.DateCreated,
//                    Description = x.pt.Description,
//                    Details = x.pt.Details,
//                    LanguageId = x.pt.LanguageId,
//                    OriginalPrice = x.p.OriginalPrice,
//                    Price = x.p.Price,
//                    SeoAlias = x.pt.SeoAlias,
//                    SeoDescription = x.pt.SeoDescription,
//                    SeoTitle = x.pt.SeoTitle,
//                    Stock = x.p.Stock,
//                    ViewCount = x.p.ViewCount,
//                    ThumbnailImage = x.pi.ImagePath
//                }).ToListAsync();

//            //4. Select and projection
//            var pagedResult = new PagedResult<ProductVm>()
//            {
//                TotalRecords = totalRow,
//                PageSize = request.PageSize,
//                PageIndex = request.PageIndex,
//                Items = data
//            };
//            return pagedResult;
//        }

//        public async Task<int> Update(ProductUpdateRequest request)
//        {
//            //1. Select join
//            var query = from p in _context.Products
//                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
//                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
//                        from pic in ppic.DefaultIfEmpty()
//                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
//                        from c in picc.DefaultIfEmpty()
//                        //join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
//                        from pi in ppi.DefaultIfEmpty()
//                        where pt.LanguageId == request.LanguageId && pi.IsDefault == true
//                        select new { p, pt, pic, pi };
//            //2. filter
//            if (!string.IsNullOrEmpty(request.Keyword))
//                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

//            if (request.CategoCryId != null && request.CategoryId != 0)
//            {
//                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
//            }

//            //3. Paging
//            int totalRow = await query.CountAsync();

//            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
//                .Take(request.PageSize)
//                .Select(x => new ProductVm()
//                {
//                    Id = x.p.Id,
//                    Name = x.pt.Name,
//                    DateCreated = x.p.DateCreated,
//                    Description = x.pt.Description,
//                    Details = x.pt.Details,
//                    LanguageId = x.pt.LanguageId,
//                    OriginalPrice = x.p.OriginalPrice,
//                    Price = x.p.Price,
//                    SeoAlias = x.pt.SeoAlias,
//                    SeoDescription = x.pt.SeoDescription,
//                    SeoTitle = x.pt.SeoTitle,
//                    Stock = x.p.Stock,
//                    ViewCount = x.p.ViewCount,
//                    ThumbnailImage = x.pi.ImagePath
//                }).ToListAsync();

//            //4. Select and projection
//            var pagedResult = new PagedResult<ProductVm>()
//            {
//                TotalRecords = totalRow,
//                PageSize = request.PageSize,
//                PageIndex = request.PageIndex,
//                Items = data
//            };
//            return pagedResult;
//        }

//        public async Task<bool> UpdatePrice(int ProductId, decimal newPrice)
//        {
//            var product = await _context.Products.FindAsync(ProductId);
//            if (product == null) throw new EShopException($"Cannot find a product with id: {ProductId}");
//            product.Price = newPrice;
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> UpdateStock(int ProductId, int addeQuantity)
//        {
//            var product = await _context.Products.FindAsync(ProductId);
//            if (product == null) throw new EShopException($"Cannot find a product with id: {ProductId}");
//            product.Stock += addeQuantity;
//            return await _context.SaveChangesAsync() > 0;
//        }
//    }
//}
