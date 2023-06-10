using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Core.Utilities.StringHelper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Entities;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using BlogApp.Services.Repositories.Auth;
using BlogApp.Services.Repositories.Schedule;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IAuthService authService, SignInManager<User> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        [HttpGet("/login")]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
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
            if (!response) return RedirectToAction("Login", "Auth");
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Register", "Auth");
            }
            ScheduleService.ScheduleSendRegisterEmail(userRegisterRequest.Email, userRegisterRequest.Name);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet("/access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/google-login")]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("ExternalResponse", "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [Route("/external-response")]
        public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/home")
        {
            var checkExternalResponse = await _authService.GoogleExternalResponse();
            if (!checkExternalResponse) return RedirectToAction("Login", "Auth");
            return Redirect(ReturnUrl);
        }
    }
}

