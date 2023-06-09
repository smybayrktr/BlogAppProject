using System;
using BlogApp.Entities;
using BlogApp.Services.Repositories.UserServiceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Services.Repositories.AuthServiceRepository
{
    public class AuthService : IAuthService
    {
        private IPasswordHasher<User> _passwordHasher;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;


        public AuthService(IPasswordHasher<User> passwordHasher, UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Register(User user)
        {
            var userToFindByEmail = await CheckUserExistsByEmail(user.Email);
            if (userToFindByEmail != null)
            {
                return false;
            }
           
            var userRegister = new User
            {
                Email = user.Email,
            };
            var hashedPassword = _passwordHasher.HashPassword(userRegister, user.Password);
            user.PasswordHash = hashedPassword;
            await _userService.Add(user);
            return true;
        }

        public async Task<bool> Login(User user)
        {
            var userToFind = await _userManager.FindByEmailAsync(user.Email);
            if (userToFind == null)
            {
                return false;
            }
            var verifyPassword = _passwordHasher.VerifyHashedPassword(userToFind, userToFind.PasswordHash, user.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                return false;
            }
            await _signInManager.PasswordSignInAsync(userToFind, user.Password, true, false);
            return true;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<User> CheckUserExistsByEmail(string email)
        {
            var userToFind = await _userService.GetByMail(email);
            if (userToFind == null)
            {
                return null;
            }

            return userToFind;
        }
    }
}

