using eShopSlution.Data.EF;
using eShopSlution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using EShopSolution.Application2.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace EShopSolution.Application2.Catalog.Products;

public class ManagerProuctService : ImanagerProductService
{
    private readonly eShopDbContext _context;
    private readonly IStorageService _storageService;

    public ManagerProuctService(eShopDbContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task AddViewCount(int ProductId)
    {
        var product = await _context.Products.FindAsync(ProductId);

        product.ViewCount += 1;
        await _context.SaveChangesAsync();
    }

    public async Task<int> Create(ProductCreateRequest request)
    {

        var product = new Product()
        {
            Price = request.Price,
            OriginalPrice = request.OriginalPrice,
            Stock = request.Stock,
            ViewCount = 0,
            DataCreated = DateTime.Now,
            SeoAlias = request.SeoAlias,
            ProductTranslations = new List<ProductTranslation>()
            {
                new ProductTranslation()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Details = request.Details
                    ,
                    SeoDescription = request.SeoDescription,
                    SeoAlias = request.SeoAlias,
                    SeoTitle     = request.SeoTitle,
                    LanguageId =   request.languageId
                }
            }

        };

        // Save Image
        _context.Products.Add(product);
         await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task<int> Delete(int ProductId)
    {
        var product = await _context.Products.FindAsync(ProductId);

        if (product == null)
        {
            throw new EShopException($"Cannt Find A Product :{ProductId}");
        }

         await _context.SaveChangesAsync();
        return product.Id;
    }


    public async Task<PageResult<ProductViewModel>> GetAllPaging(GetPublicProductPagingRequest request)
    {
        //1 select join
        var query = from p in _context.Products
                    join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                    join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                    join c in _context.Categories on pic.CategoryId equals c.Id
                    select new { p, pt, pic };

        //2 Filer
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            query = query.Where(x => x.pt.Name.Contains(request.Keyword));
        }
        if (request.CategoryId != null && request.CategoryId !=0)
        {
            query = query.Where(p => p.pic.CategoryId == request.CategoryId);
        }
        //3 Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
            .Select(x => new ProductViewModel()
            {
                Id = x.p.Id,    
                Name = x.pt.Name,
                DateCreated = x.p.DataCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.pt.OriginalPrice,
                Price = x.p.OriginalPrice,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount
            }).ToListAsync();

        //4 Select and projection
        var pageResult = new PageResult<ProductViewModel>()
        {
            TotalRecord = totalRow,
            Items = data,
        };
        return pageResult;

    }

    public async Task<ProductViewModel> GetById(int productId, string languageId)
    {
        var product = await _context.Products.FindAsync(productId);
        var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);;

        var productViewModel = new ProductViewModel()
        {
            Id = product.Id,
            DateCreated = product.DataCreated,
            Description = productTranslation != null ? productTranslation.Description : null,
            LanguageId = productTranslation.LanguageId,
            Details = productTranslation != null ? productTranslation.Details : null,
            Name = productTranslation != null ? productTranslation.Name : null,
            OriginalPrice = product.OriginalPrice,
            Price = product.Price,
            SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
            SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
            SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
            Stock = product.Stock,
            ViewCount = product.ViewCount
        }
        ;
        return productViewModel;

        //throw new NotImplementedException();
    }

    public async Task<ProductViewModel> AddImages(int ProductId)
    {
       throw new NotImplementedException();
    }

    public async Task<int> Update(ProductUpdateRequest request)
    {
        var product = await _context.Products.FindAsync(request.Id);
        var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
        if (product == null || productTranslations == null)
        {
            throw new EShopException($"Cannt Find A Product with id :{request.Id}");
        }

        productTranslations.Name = request.Name;
        productTranslations.SeoAlias = request.SeoAlias;
        productTranslations.SeoDescription = request.SeoDescription;
        productTranslations.SeoTitle = request.SeoTitle;
        productTranslations.Description = request.Description;
        productTranslations.Details = request.Details;
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdatePrice(int ProductId, decimal newPrice)
    {
        var product = await _context.Products.FindAsync(ProductId);
        if (product == null)
        {
            throw new EShopException($"Cannt Find A Product with id :{ProductId}");
        }
        product.Price = newPrice;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateStock(int ProductId, int addeQuantity)
    {
        var product = await _context.Products.FindAsync(ProductId);
        if (product == null)
        {
            throw new EShopException($"Cannt Find A Product with id :{ProductId}");
        }
        product.Price += addeQuantity;
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + "USER_CONTENT_FOLDER_NAME" + "/" + fileName;
    }


}
