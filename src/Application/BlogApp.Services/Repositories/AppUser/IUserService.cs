using System;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.User
{
	public interface IUserService
	{
        Task<IList<User>> GetAll();
        Task<User> GetById(int id);
        Task AddAsync(User user);
        Task Update(User user);
        Task Delete(User user);
        Task<User> GetByMail(string email);
    }
}

