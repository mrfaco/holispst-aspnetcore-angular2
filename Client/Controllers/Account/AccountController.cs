using Client.UsersContext;
using Client.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers.Account
{
    public class AccountController: Controller
    {
        private UserManager<HolisUser> _userManager;
        private SignInManager<HolisUser> _signInManager;
        private HolisUser _currentUser;

        public AccountController(UserManager<HolisUser> userManager, SignInManager<HolisUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new HolisUser() { UserName = model.UserName };
                //var create = await _userManager.CreateAsync(user, model.Password);
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true,lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index),"Home");
                }
                
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            this._currentUser = null;
            return RedirectToAction("Account/LogIn");
        }

        [HttpGet]
        public IActionResult User()
        {
            if (_currentUser == null)
            {
                GetCurrentUser().Wait();
            }
            if (this._currentUser != null)
            {
                return (Ok(this._currentUser));
            }
            else
            {
                return (Ok(null));
            }
        }

        private async Task GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            this._currentUser = user;
        }
    }
}
