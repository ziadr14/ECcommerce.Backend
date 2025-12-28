using ECom.API.Helper;
using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _services;

        public CategoriesController(ICategoryServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _services.GetAllAsync();

            return Ok(new
            {
                response = new ResponseApi(200),
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _services.GetById(id);
            if (data == null)
                return NotFound(new ResponseApi(404));

            return Ok(new
            {
                response = new ResponseApi(200),
                data
            });
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            await _services.CreateCategory(dto);

            return StatusCode(201, new ResponseApi(201));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto dto)
        {
            await _services.Update(dto);

            return Ok(new ResponseApi(200, "Category Updated Successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             await _services.DeleteById(id);



            return Ok(new ResponseApi(200, "Category Deleted Successfully"));
        }
    }
}
