using eCommerce.Application.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        // GET api/users/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            if (userId  == Guid.Empty) return BadRequest("Invalid user");
            var user = await _service.GetUser(userId);
            if (user == null) return NotFound(user);
            return Ok(user);
        }

    }
}
