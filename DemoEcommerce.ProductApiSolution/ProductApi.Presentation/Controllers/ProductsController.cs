using Ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Dtos.Convertions;
using ProductApi.Application.Dtos.Products;
using ProductApi.Application.Interfaces;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _productRepository;

        public ProductsController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            if (!products.Any())
                return NotFound("No products found");

            var list = ProductConversion.FromEntities(products);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productRepository.FindByIdAsync(id);

            if (product is null)
                return NotFound("Product not found");

            var single = ProductConversion.FromEntity(product);
            return Ok(single);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = ProductConversion.ToEntity(productDto);
            var response = await _productRepository.CreateAsync(product);

            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateProduct(ProductUpdateDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = ProductConversion.ToEntity(productDto);
            var response = await _productRepository.UpdateAsync(product);

            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteProduct(Guid id)
        {
            var response = await _productRepository.DeleteAsync(id);

            return response.Flag is true ? Ok(response) : BadRequest(response);
        }
    }
}