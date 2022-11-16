using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.Models;
using TrainScheduler.Model.ViewModels;
using System.Linq;
using TrainScheduler.Model.Enums;
using TrainScheduler.Model.Exceptions;

namespace TrainScheduler.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel registerModel)
        {
            var user = new IdentityUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var identityResult = await _userManager.CreateAsync(user, registerModel.Password);

            if (identityResult.Succeeded)
            {
                var userRole = await _roleManager.FindByNameAsync(RoleNames.User.ToString());
                if (userRole == null)
                {
                    throw new ObjectNotFoundException("User Role");
                }

                await _userManager.AddToRoleAsync(user, userRole.Name);
            }

            return new AuthResult(identityResult.Succeeded) 
            { 
                Errors = identityResult.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<AuthResult> SignInAsync(LoginModel loginModel)
        {
            var result = new AuthResult(true);

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                result.Errors.Add("User or password is not correct.");
                result.Succeeded = false;
                return result;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);
            if (!signInResult.Succeeded)
            {
                result.Errors.Add("User or password is not correct.");
                result.Succeeded = false;
            }

            return result;
        }

        public Task SignOut()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
