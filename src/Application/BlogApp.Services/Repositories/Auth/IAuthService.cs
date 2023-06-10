using System;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.Auth
{
	public interface IAuthService
	{
        Task<bool> Register(UserRegisterRequest userRegisterRequest);
        Task<bool> Login(UserLoginRequest userLoginRequest);
        Task<bool> GoogleExternalResponse();
        Task Logout();
    }
}

