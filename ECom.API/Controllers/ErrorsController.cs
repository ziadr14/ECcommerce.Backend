using ECom.API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [AllowAnonymous] 
    [Route("errors/{statusCode}")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetErrors(int statusCode)
        {
            return new ObjectResult(new ResponseApi(statusCode))
            {
                StatusCode = statusCode
            };
        }
    }
}
