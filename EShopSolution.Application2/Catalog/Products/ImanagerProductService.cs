using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;

namespace EShopSolution.Application2.Catalog.Products
{
    public interface ImanagerProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int ProductId);
        Task AddViewCount(int ProductId);
        Task<bool> UpdatePrice(int ProductId, decimal newPrice);
        Task<bool> UpdateStock(int ProductId, int addeQuantity);



        Task<PageResult<ProductViewModel>> GetAllPaging(GetPublicProductPagingRequest request);
    }

}
