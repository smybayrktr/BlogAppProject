using System;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.AppUser
{
	public interface IUserService
	{
        Task<IList<User?>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user, string password);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetCurrentUser();
    }
}

