using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Entities;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl=returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User
            {
                Email = model.Email.ToLower(),
                Password = model.Password
            };
            var loginResult = await _authService.Login(user);
            if (!loginResult)
            {
                ModelState.AddModelError("Login", "Sisteme giriş yapılamadı.");
                return View(model);
            }
            Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Role)
                    };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        } 


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterViewModel);
            }
            var user = new User
            {
                Name= userRegisterViewModel.Name,
                LastName = userRegisterViewModel.LastName,
                Email = userRegisterViewModel.Email,
                Password = userRegisterViewModel.Password
            };
            var result = await _authService.Register(user);
            if (!result)
            {
                return View(userRegisterViewModel);
            }

            var userLogin = new User
            {
                Email = userRegisterViewModel.Email,
                Password = userRegisterViewModel.Password
            };
            var loginResult = await _authService.Login(userLogin);
            if (!loginResult)
            {
                return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
           await _authService.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

