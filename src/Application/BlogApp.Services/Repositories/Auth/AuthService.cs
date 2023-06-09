using System;
using System.Reflection.Metadata;
using AutoMapper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Entities;
using BlogApp.Services.Extensions;
using BlogApp.Services.Repositories.AppUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Services.Repositories.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public AuthService(IPasswordHasher<User> passwordHasher, SignInManager<User> signInManager,
            IUserService userService, IMapper mapper)
        {
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _userService = userService;
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

        private bool verifyUserPassword(User user, string hashedPassword, string providedPassword )
        {
            var verifyPassword = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return verifyPassword == PasswordVerificationResult.Failed ? false : true;
        }
    }
}

