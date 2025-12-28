using ECom.API.Helper;
using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using ECom.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;

        public ProductsController(IProductService product)
        {
            _product = product;
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductParams productParams)
        {
    

            var result = await _product.GetAll(productParams);

            return Ok(result);
            



        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _product.GetById(id);

            if (data == null)
                return NotFound(new ResponseApi(404));

            return Ok(new
            {
                response = new ResponseApi(200),
                data
            });
        }
        [HttpGet("{id}/similar")]
        public async Task<IActionResult> GetSimilarProducts(
            int id,
            [FromQuery] ProductParams productParams
        )
        {
            var result = await _product.GetSimilarProducts(id, productParams);
            return Ok(result);
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseApi(400, "Invalid product data"));

            await _product.CreateProduct(dto);

            return StatusCode(201, new ResponseApi(201, "Product created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateProductDto dto)
        {
            await _product.UpdateProduct(dto);

            return Ok(new ResponseApi(200, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _product.DeleteProduct(id);

            return Ok(new ResponseApi(200, "Product deleted successfully"));
        }
    }
}
