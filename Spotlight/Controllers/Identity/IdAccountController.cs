using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotlight.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Spotlight.Controllers.Identity
{
    [Route("Identity/[controller]/[action]/{id?}")]
    [Authorize]
    public class IdAccountController : Controller
    {
        private IConfiguration configuration;    
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private readonly IWebHostEnvironment hostEnvironment;
        public IdAccountController(UserManager<AppUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            SignInManager<AppUser> signinMgr,
            IConfiguration IConfig,
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passValid,
            IPasswordHasher<AppUser> passwordHash,
            IWebHostEnvironment hostEnvironment)
        {
            roleManager = roleMgr;
            userManager = userMgr;
            signInManager = signinMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            configuration = IConfig;
            this.hostEnvironment = hostEnvironment;
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

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "You must have a confirmed email to log on.");
                    return View("~/Views/Identity/Account/Login.cshtml", details);
                }

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
        public ViewResult Register()
        {
            List<string> roles = roleManager.Roles.Select(x => x.Name).ToList();
            roles.Remove("Admins");
            List<SelectListItem> Roles = roles.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            ViewBag.ViewRoles = Roles;
            return View("~/Views/Identity/Account/Register.cshtml");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            //Console.WriteLine("Selected role check: "+model.Role);
            List<string> roles = roleManager.Roles.Select(x => x.Name).ToList();
            roles.Remove("Admins");
            List<SelectListItem> Roles = roles.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            ViewBag.ViewRoles = Roles;
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
                    ViewBag.ErrorTitle = "Registration Successful";
                    this.SendEmail(user);             
                    
                    await userManager.AddToRoleAsync(user, model.Role);
                    //return RedirectToAction("Login", "IdAccount");
                    return View("~/Views/Identity/Account/EmailConfirm.cshtml");
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
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                return View("~/Views/Identity/Account/Edit.cshtml", new Models.Identity.EditModel { Name = user.UserName, Email = user.Email, Password="" , ImageName = user.ImageName});
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        [HttpPost]
        //public async Task<IActionResult> Edit(string username, string email, string password, IFormFile image)
        public async Task<IActionResult> Edit(EditModel newedit)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            //Console.WriteLine("PROFILE: " + newedit.Name + newedit.Email + newedit.Password + newedit.ImageFile.FileName);
            if (newedit.ImageFile != null)
            {
                //Console.WriteLine("PROFILE: " + newedit.ImageFile.FileName);
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(newedit.ImageFile.FileName);
                string extension = Path.GetExtension(newedit.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmss") + extension;
                user.ImageName = fileName;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await newedit.ImageFile.CopyToAsync(fileStream);
                }
            }

            if (user != null)
            {
                user.UserName = newedit.Name;
                user.Email = newedit.Email;
                IdentityResult validEmail
                = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(newedit.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager,
                    user, newedit.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,
                        newedit.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                || (validEmail.Succeeded
                && newedit.Password != string.Empty && validPass.Succeeded))
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string token)
        {
            if(userId ==null || token ==null)
                return RedirectToAction("Index", "Home");

            var user = await userManager.FindByIdAsync(userId);

            if (user == null) {
                ViewBag.ErrorTitle = "Not Found";
                ViewBag.ErrorMessage = "The User ID " + userId + " is invalid";
                return View("~/Views/Identity/Account/EmailConfirm.cshtml");
            }
            var result = await userManager.ConfirmEmailAsync(user,token);

            ViewBag.ErrorTitle = "You confirmed your email successfully";
            ViewBag.ErrorMessage = "Email: " + user.Email + " is confirmed. You can now login.";

            return View("~/Views/Identity/Account/EmailConfirm.cshtml");
        }

        public async void SendEmail(AppUser user) {       
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "IdAccount",
                new { userId = user.Id, token = token }, Request.Scheme);
            Console.WriteLine("CONFIRMATION LINK: " + confirmationLink);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;

            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(configuration["AppEmail:Email"], configuration["AppEmail:Password"]);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //configuration["AdminUser:Name"]
            MailMessage mailMessage = new MailMessage(configuration["AppEmail:Email"], user.Email);
            mailMessage.Subject = "Spotlight: Confirm your email!";
            mailMessage.Body = "Please confirm your email, by cliking on the confirmation link: " + confirmationLink;
            mailMessage.IsBodyHtml = false;
            try
            {              
                smtpClient.Send(mailMessage);
                Console.WriteLine("EMAIL SENT to: " + user.Email);
                ViewBag.ErrorMessage = "Before you can login, please confirm your email, by cliking on the confirmation link. (HINT: link is also in console)";
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP ERROR: " + ex.ToString());
            }          
        }
    }
}