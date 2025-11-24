using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _service;
        private IValidator<LoginRequest> _loginValidator;
        private IValidator<RegisterRequest> _registerValidator;

        public AuthController(IUsersService service, 
            IValidator<LoginRequest> loginValidator, 
            IValidator<RegisterRequest> registerValidator)
        {
            _service = service;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest register)
        {
            if (register is null) return BadRequest("Invalid registration data");

            var validationResult = await _registerValidator.ValidateAsync(register);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var authenticationResponse = await _service.Register(register);

            if (authenticationResponse is null || authenticationResponse.Success is false)
                return BadRequest(authenticationResponse);

            return Ok(authenticationResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            if (login is null) return BadRequest("Invalid credentials data");

            var validationResult = await _loginValidator.ValidateAsync(login);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var authenticationResponse = await _service.Login(login);

            if (authenticationResponse is null || authenticationResponse.Success is false)
                return Unauthorized(authenticationResponse);
            
            return Ok(authenticationResponse);
        }
    }
}
