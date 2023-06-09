using System;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.BlogAction { 

	public interface ISavedBlogService
    {
        Task<SaveBlogResponse> UserSaveAction(CreateSaveBlogRequest createSaveBlogRequest);
        Task<SavedBlog?> GetSavedBlogByBlogIdAsync(int blogId);
        Task<IEnumerable<SavedBlog?>> GetSavedBlogsByUserAsync();
    }
}

