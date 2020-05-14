using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotlight.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Spotlight.Controllers.Identity
{
    [Route("Identity/[controller]/[action]/{id?}")]
    [Authorize]
    public class IdAccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        public IdAccountController(UserManager<AppUser> userMgr,
            SignInManager<AppUser> signinMgr,
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passValid,
            IPasswordHasher<AppUser> passwordHash)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View("~/Views/Identity/Account/Login.cshtml");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(
                    user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email),
                "Invalid user or password");
            }
            return View("~/Views/Identity/Account/Login.cshtml",details);
        }
            
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ViewResult Register() => View("~/Views/Identity/Account/Register.cshtml");

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                //not great solution to check first confirm password and report error, and then everything else
                if (model.ConfirmPassword != model.Password)
                {
                    ModelState.AddModelError("", "Confirm password doesn't match password");
                    return View("~/Views/Identity/Account/Register.cshtml");
                }
                IdentityResult result
                = await userManager.CreateAsync(user, model.Password);              
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "IdAccount");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("~/Views/Identity/Account/Register.cshtml",model);
        }

        public async Task<IActionResult> Edit()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                return View("~/Views/Identity/Account/Edit.cshtml", user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string username, string email, string password)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                user.UserName = username;
                user.Email = email;
                IdentityResult validEmail
                = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager,
                    user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,
                        password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                || (validEmail.Succeeded
                && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {                     
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("~/Views/Identity/Account/Edit.cshtml", user);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [Route("/Account/AccessDenied/{id?}")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Identity/Account/AccessDenied.cshtml");
        }

    }
}