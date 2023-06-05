using System;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services
{
	public interface IBlogService
	{
        IEnumerable<BlogCardResponse> GetBlogsCardResponses();
        IEnumerable<BlogCardResponse> GetBlogsByCategory(int categoryId);
        BlogCardResponse GetBlog(int id);
        Task CreateBlogAsync(CreateNewBlogRequest newBlogRequest);
    }
}

