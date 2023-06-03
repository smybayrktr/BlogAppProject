using System;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services
{
	public interface IBlogService
	{
        IEnumerable<BlogCardResponse> GetBlogsCardResponses();
        IEnumerable<BlogCardResponse> GetBlogsByCategory(int categoryId);
    }
}

