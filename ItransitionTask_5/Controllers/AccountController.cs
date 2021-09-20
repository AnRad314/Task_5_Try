using ItransitionTask_5.Data;
using ItransitionTask_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItransitionTask_5.Controllers
{
	public class AccountController : Controller
	{
        private GameDbContext _context;
        private readonly UserManager<UserGame> _userManager;
        private readonly SignInManager<UserGame> _signInManager;

        public AccountController(GameDbContext context, UserManager<UserGame> userManager, SignInManager<UserGame> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ExternalAuthentication(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UserGame user = new UserGame
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Login = info.Principal.FindFirstValue(ClaimTypes.Name)
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddLoginAsync(user, info);
                Claim claim = new Claim("LoginName", info.Principal.FindFirstValue(ClaimTypes.Name));
                await _userManager.AddClaimAsync(user, claim);
                await _signInManager.SignInAsync(user, false);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
