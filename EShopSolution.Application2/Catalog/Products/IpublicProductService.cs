using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Application2.Catalog.Products
{
    public interface IpublicProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        public  Task<IEnumerable<ProductViewModel>> GetAll(string languageId);
    }
}
