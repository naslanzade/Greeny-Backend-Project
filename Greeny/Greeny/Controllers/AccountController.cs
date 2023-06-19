﻿using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels.Account;
using Greeny.ViewModels.ForgetPass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Greeny.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IEmailService emailService,RoleManager<IdentityRole> roleManager)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = new()
            {
                Email = request.Email,
                FullName= request.FullName,
                UserName=request.UserName,
            };

            var result=await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(request);
            }

            //await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
            //await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/account.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);

            html = html.Replace("{{fullname}}", user.FullName);

            string subject = "Email Confirmation";

            _emailService.Send(user.Email, subject, html);


            await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(VerifyEmail));

        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult VerifyEmail()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginVM request)
        {
            if (!ModelState.IsValid) 
            {
                return View(request);
            }

            AppUser user= await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect email address or password, please try again");
                return View(request);
            }

            PasswordVerificationResult comparedPassword = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (comparedPassword.ToString() == "failed")
            {
                ModelState.AddModelError(string.Empty, "Incorrect email address or password, please try again");
                return View(request);

            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Please confirm email");
                return View(request);
            }

            return RedirectToAction("Index", "Home");

            
        }


        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }

            }

            return Ok();
        }


        [HttpGet]
        public IActionResult ForgetPassword() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM request)
        {
            //if (!ModelState.IsValid) return View();
           
            AppUser user=await _userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                ModelState.AddModelError("Error", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, token }, Request.Scheme);


            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);

            html = html.Replace("{{fullname}}", user.FullName);

            string subject = "Reset Password";

            _emailService.Send(user.Email, subject, html);


            await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(VerifyEmail));

        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResestPasswordVM { UserId = userId, Token = token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmResetPassword(ResestPasswordVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return RedirectToAction(nameof(SignIn));
        }






    }
}
