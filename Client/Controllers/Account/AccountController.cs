using Client.UsersContext;
using Client.Utils;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers.Account
{
    public class AccountController: Controller
    {
        private UserManager<HolisUser> _userManager;
        private SignInManager<HolisUser> _signInManager;
        private HolisUser _currentUser;
        private RoleManager<IdentityRole> _rolesManager;
        private IConfigurationRoot _config;
        private IMemoryCache _memoryCache;

        public AccountController(UserManager<HolisUser> userManager, SignInManager<HolisUser> signInManager, RoleManager<IdentityRole> rolesManager, IConfigurationRoot config, IMemoryCache memoryCache)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._rolesManager = rolesManager;
            this._config = config;
            this._memoryCache = memoryCache;
        }

        private async Task<string> GetAuthenticationToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("username", "TEST"));
                nvc.Add(new KeyValuePair<string, string>("password", "TEST123"));

                var response = await httpClient.PostAsync("/token", new FormUrlEncodedContent(nvc));

                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Auth>(data).access_token;
            }
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
                    var token = await GetAuthenticationToken();
                    StoreCookie(token);
                    return RedirectToAction(nameof(HomeController.Index),"Home");
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        private void StoreCookie(string token)
        {
            _memoryCache.Set("token", token);
        }

        private void DeleteTokenCookie()
        {
            _memoryCache.Set("token", string.Empty);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            this._currentUser = null;
            DeleteTokenCookie();
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

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new HolisUser() { UserName = model.UserName };
                var create = await _userManager.CreateAsync(user, model.Password);
                if (create.Succeeded)
                {
                    ViewBag.Success = "Added Successfully";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Failed to create user. Reason: {create.Errors.FirstOrDefault().Description}");
                }
            }
            return View();
        }
    }
}
