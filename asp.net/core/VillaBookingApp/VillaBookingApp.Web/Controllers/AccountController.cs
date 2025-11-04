using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaBookingApp.Application.Utility;
using VillaBookingApp.Domain.Entities;
using VillaBookingApp.Web.Models;

namespace VillaBookingApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginViewModel login = new ()
            {
                RedirectUrl = returnUrl,
            };
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    login.Email,
                    login.Password,
                    login.Remember, // isPersistent
                    false // lockOutOnFailure
                );
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(login.RedirectUrl))
                    {
                        return LocalRedirect(login.RedirectUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View(login);
        }

        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            //var roleExist = await _roleManager.RoleExistsAsync("Admin"); // magic strings nay
            var roleExist = await _roleManager.RoleExistsAsync(SD.RoleAdmin); // constants yay!
            if (!roleExist) 
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.RoleAdmin));
                await _roleManager.CreateAsync(new IdentityRole(SD.RoleCustomer));
            }

            RegisterViewModel register = new()
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }),
                RedirectUrl = returnUrl
            };
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    Name = register.Name,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    NormalizedEmail = register.Email.ToUpper(),
                    EmailConfirmed = true,
                    UserName = register.Email,
                    CreatedAt = DateTime.Now,
                };
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(register.Role))
                    {
                        await _userManager.AddToRoleAsync(user, register.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.RoleCustomer);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if (!string.IsNullOrEmpty(register.RedirectUrl))
                    {
                        return LocalRedirect(register.RedirectUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            register.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            });
            return View(register);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
