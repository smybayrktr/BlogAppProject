using System;
using BlogApp.Entities;

namespace BlogApp.Services
{
	public interface IAuthService
	{
        Task<bool> Login(User user);
        Task<bool> Register(User user);
        Task Logout();
        Task<User> CheckUserExistsByEmail(string email);
    }
}

