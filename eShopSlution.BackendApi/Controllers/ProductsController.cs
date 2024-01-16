using EShopSolution.Application2.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSlution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IpublicProductService _ipublicProductService;

        public ProductsController(IpublicProductService ipublicProductService)
        {
            _ipublicProductService = ipublicProductService;
        }
        //public IActionResult Index()
        //{

        //    return Ok("abc");
        //}
        
        [HttpGet("/abc")]
        public async Task<IActionResult> Get()
        {
            var data = await _ipublicProductService.GetAll();

            return Ok(data);
        }
    }
}
