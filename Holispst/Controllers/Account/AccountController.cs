using Entities;
using Holispst.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holispst.Controllers.Account
{
    public class AccountController: Controller
    {
        private SignInManager<HolisUser> _signInManager;
        private UserManager<HolisUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<HolisUser> _userManager, RoleManager<IdentityRole> _roleManager,
            SignInManager<HolisUser> _signInManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
        }

        [HttpPost]
        public IActionResult Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            return (BadRequest());
        }
    }
}
