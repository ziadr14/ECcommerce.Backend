using AutoMapper;
using ECom.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers.Bugs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public BugController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        // --------------------------------------------------------------------
        // 1) Not Found (Category)
        // --------------------------------------------------------------------
        [HttpGet("category-not-found")]
        public async Task<ActionResult> GetCategoryNotFound()
        {
            var category = await _work.Categories.GetById(9999);

            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        // --------------------------------------------------------------------
        // 2) Not Found (Product)
        // --------------------------------------------------------------------
        [HttpGet("product-not-found")]
        public async Task<ActionResult> GetProductNotFound()
        {
            var product = await _work.Products.GetById(9999);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        // --------------------------------------------------------------------
        // 3) Server Error (Category)
        // --------------------------------------------------------------------
        [HttpGet("category-server-error")]
        public async Task<ActionResult> GetCategoryServerError()
        {
            var category = await _work.Categories.GetById(1);

            // عمل خطأ متعمد
            var x = category.Name.ToLower(); // لو Name = null → Exception

            return Ok(category);
        }

        // --------------------------------------------------------------------
        // 4) Server Error (Product)
        // --------------------------------------------------------------------
        [HttpGet("product-server-error")]
        public async Task<ActionResult> GetProductServerError()
        {
            var product = await _work.Products.GetById(1);

            var x = product.Description.ToLower();

            return Ok(product);
        }


        [HttpGet("category-bad-request/{id}")]
        public ActionResult GetCategoryBadRequest(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid category id");

            return Ok(id);
        }

        // --------------------------------------------------------------------
        // 6) Bad Request (Product)
        // --------------------------------------------------------------------
        [HttpGet("product-bad-request/{id}")]
        public ActionResult GetProductBadRequest(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product id");

            return Ok(id);
        }

        // --------------------------------------------------------------------
        // 7) Generic Bad Request
        // --------------------------------------------------------------------
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("This is a bad request test");
        }
    }
}
    

