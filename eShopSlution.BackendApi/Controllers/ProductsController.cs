using eShopSolution.ViewModels.Catalog.Products;
using EShopSolution.Application2.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace eShopSlution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IpublicProductService _ipublicProductService;
        private readonly ImanagerProductService _imanagerProductService;

        public ProductsController(IpublicProductService ipublicProductService, ImanagerProductService imanagerProductService)
        {
            _ipublicProductService = ipublicProductService;
            _imanagerProductService = imanagerProductService;
        }
        //public IActionResult Index()
        //{

        //    return Ok("abc");
        //}

        [HttpGet("{languageId}")]
        public async Task<IActionResult> Get(string languageId)
        {
            var data = await _ipublicProductService.GetAll(languageId);

            return Ok(data);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var data = await _ipublicProductService.GetAllByCategoryId(request);

            return Ok(data);
        }

        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetById(int id, string languageId)
        {
            var products = await _imanagerProductService.GetById(id, languageId);
            if (products == null)
            {
                return BadRequest("Cannot find product"); //Request 404
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _imanagerProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }

            var product = await _imanagerProductService.GetById(productId , request.languageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, product);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var affectedResult = await _imanagerProductService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            var product = await _imanagerProductService.GetById(affectedResult, request.LanguageId);

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedResult = await _imanagerProductService.Delete(id);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();

        }

        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSuccessful = await _imanagerProductService.UpdatePrice(id, newPrice);
            if (isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        } 
        }
    }
