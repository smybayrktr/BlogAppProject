using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Entities;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using BlogApp.Services.Repositories.Auth;
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
        [HttpGet("/login")]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl=returnUrl;
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(UserLoginViewModel model, string? returnUrl)
        {
            UserLoginRequest userLoginRequest = new UserLoginRequest()
            {
                Email = model.Email,
                Password = model.Password
            };
            var response = await _authService.Login(userLoginRequest);
            return RedirectToAction("Index","Home");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterViewModel);
            }
            UserRegisterRequest userRegisterRequest = new UserRegisterRequest()
            {
                Email = userRegisterViewModel.Email,
                 LastName = userRegisterViewModel.LastName,
                  Name = userRegisterViewModel.Name,
                   Password = userRegisterViewModel.Password

            };
            var loginResult = await _authService.Register(userRegisterRequest);
            if (!loginResult)
            {
                return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
           await _authService.Logout();
           return RedirectToAction("Login", "Auth");
        }
        [HttpGet("/access-denied-auth")]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

