using System;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services.Repositories.BlogAction { 

	public interface IBlogActionService
	{
		Task<BlogActionResponse> LikeAsync(CreateBlogActionRequest createBlogActionRequest);

        Task<BlogActionResponse> DislikeAsync(CreateBlogActionRequest createBlogActionRequest);

    }
}

