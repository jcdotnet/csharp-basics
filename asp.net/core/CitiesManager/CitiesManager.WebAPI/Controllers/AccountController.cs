using CitiesManager.Core.DTO;
using CitiesManager.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        /// <summary>
        /// Register a user in the database
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDto userDto)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => 
                    v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            ApplicationUser appUser = new ApplicationUser()
            {
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                UserName = userDto.Email,
                Name = userDto.Name
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, userDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);

                return Ok(appUser);
            }
            else
            {
                return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
            }
        }

        /// <summary>
        /// sign a user in
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDto userDto)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v =>
                    v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password, 
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser? appUser = await _userManager.FindByEmailAsync(userDto.Email);
                if (appUser == null) // unlikely to happen
                {
                    return NoContent();
                }
                return Ok(new { Name = appUser.Name, Email = appUser.Email});
            }
            else
            {
                return Problem("Invalid credentials");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ValidateEmail(string email)
        {
            ApplicationUser? appUser = await _userManager.FindByEmailAsync(email);

            return Ok(appUser == null);
        }
    }
}
