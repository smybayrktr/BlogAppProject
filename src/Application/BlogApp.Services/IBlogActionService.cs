using System;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services
{
	public interface IBlogActionService
	{
		Task<BlogActionResponse> Like(CreateBlogActionRequest createBlogActionRequest);

        Task<BlogActionResponse> Dislike(CreateBlogActionRequest createBlogActionRequest);

    }
}

