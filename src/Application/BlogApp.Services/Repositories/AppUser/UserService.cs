using System;
using BlogApp.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BlogApp.Infrastructure.Repositories;
using User = BlogApp.Entities.User;

namespace BlogApp.Services.Repositories.User
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<IList<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
            
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<User> GetByMail(string email)
        {
            var result = await _userRepository.GetWithPredicateAsync(x => x.Email.ToLower() == email.ToLower());
            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task AddAsync(User user)
        {
            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            await _userRepository.CreateAsync(user);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task Update(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
        public async Task Delete(User user)
        {
            await _userRepository.DeleteAsync(user);
        }

    }
}

