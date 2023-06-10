using System;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services.Repositories.Blog
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogCardResponse?>> GetBlogsCardResponsesAsync();
        Task<IEnumerable<BlogCardResponse?>> GetBlogsByCategoryAsync(int categoryId);
        Task<BlogCardResponse?> GetBlogAsync(int id);
        Task CreateBlogAsync(CreateNewBlogRequest newBlogRequest);
        Task<IEnumerable<BlogCardResponse?>> GetBlogsByUserAsync();
        Task<IEnumerable<BlogCardResponse?>> GetSavedBlogsAsync();
        Task<BlogCardResponse?> GetBlogByUrlAsync(string url);
        Task DeleteAsync(int id);
        Task<bool> UpdateAsync(UpdateBlogRequest updateBlogRequest);
        Task<UpdateBlogRequest?> GetBlogAsUpdateBlogDtoAsync(int id);
    }
}

