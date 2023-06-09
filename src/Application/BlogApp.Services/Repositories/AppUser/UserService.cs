using System;
using BlogApp.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BlogApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Services.Repositories.AppUser
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<User?>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
            
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _userRepository.GetWithPredicateAsync(x => x.Email.ToLower() == email.ToLower());
            return result == null ? null : result;
        }

        public async Task AddAsync(User user, string password)
        {
            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _userRepository.CreateAsync(user);
            await AddUserClaimsAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
        }
        private async Task AddUserClaimsAsync(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            await _userManager.AddClaimsAsync(user, claims);

        }

        public async Task<User?> GetCurrentUser()
        {
            var currentUser= _httpContextAccessor.HttpContext.User;
            if (currentUser == null)
            {
                return null;
            }
            string userEmail = currentUser?.FindFirstValue(ClaimTypes.Email);
            if (String.IsNullOrWhiteSpace(userEmail))
            {
                return null;
            }
            return await GetByEmailAsync(userEmail);
        }
    }
}

