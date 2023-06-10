using System;
using System.Reflection.Metadata;
using System.Security.Claims;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using AutoMapper;
using BlogApp.Core.Utilities.Auth0Helper;
using BlogApp.Core.Utilities.StringHelper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Entities;
using BlogApp.Services.Extensions;
using BlogApp.Services.Repositories.AppUser;
using BlogApp.Services.Repositories.Schedule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BlogApp.Services.Repositories.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly Auth0Settings _auth0Settings;


        public AuthService(IPasswordHasher<User> passwordHasher, SignInManager<User> signInManager,
            IUserService userService, IMapper mapper, UserManager<User> userManager, Auth0Settings auth0Settings)
        {
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _userService = userService;
            _userManager = userManager;
            _auth0Settings = auth0Settings;
            _mapper = mapper;
        }

        public async Task<bool> Register(UserRegisterRequest userRegisterRequest)
        {
            var checkUserByEmail = await checkUserExistsByEmail(userRegisterRequest.Email);
            if (checkUserByEmail)
            {
                return false;
            }
            var user = userRegisterRequest.ConvertToDto(_mapper);
            await _userService.AddAsync(user, userRegisterRequest.Password);
            await _signInManager.PasswordSignInAsync(user, userRegisterRequest.Password, true, false);
            return true;
        }

        public async Task<bool> Login(UserLoginRequest userLoginRequest)
        {
            var userToFind = await _userService.GetByEmailAsync(userLoginRequest.Email);
            if (userToFind == null)
            {
                return false;
            }
            var checkPassword = verifyUserPassword(userToFind, userToFind.PasswordHash, userLoginRequest.Password);
            if (!checkPassword)
            {
                return false;
            }
            await _signInManager.PasswordSignInAsync(userToFind, userLoginRequest.Password, true, false);
            return true;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<bool> checkUserExistsByEmail(string email)
        {
            var checkUserByEmail = await _userService.GetByEmailAsync(email);
            return checkUserByEmail == null ? false : true;
        }

        private bool verifyUserPassword(User user, string hashedPassword, string providedPassword)
        {
            var verifyPassword = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return verifyPassword == PasswordVerificationResult.Failed ? false : true;
        }

        public async Task<bool> GoogleExternalResponse()
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();

            if (loginInfo == null) return false;

            var loginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);

            if (loginResult.Succeeded) return true;

            var user = new User
            {
                Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                Name = loginInfo.Principal.FindFirst(ClaimTypes.Name).Value,
                LastName = loginInfo.Principal.FindFirst(ClaimTypes.Surname).Value,
            };

            var userPassword = StringHelper.GenerateRandomPassword();
            await _userService.AddAsync(user, userPassword);
            var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
            ScheduleService.ScheduleSendRegisterEmailWithPassword(user.Email, user.Name, userPassword);

            await _signInManager.SignInAsync(user, true);
            return true;
        }
    }
}

