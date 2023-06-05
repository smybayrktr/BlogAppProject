using System;
using AutoMapper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Services.Extensions;

namespace BlogApp.Services
{
	public class BlogService: IBlogService
	{
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task CreateBlogAsync(CreateNewBlogRequest newBlogRequest)
        {
            var blog = newBlogRequest.ConvertToDto(_mapper);
            await _blogRepository.CreateAsync(blog);
        }

        public BlogCardResponse GetBlog(int id)
        {
            var blog=_blogRepository.Get(id);
            var response = blog.ConvertToDto(_mapper);
            return response;
        }

        public IEnumerable<BlogCardResponse> GetBlogsByCategory(int categoryId)
        {
            var blogs = _blogRepository.GetBlogsByCategory(categoryId);
            var responses = blogs.ConvertToDto(_mapper);
            return responses;
        }

        public IEnumerable<BlogCardResponse> GetBlogsCardResponses()
        {
            var blogs = _blogRepository.GetAll();
            var responses = blogs.ConvertToDto(_mapper);
            return responses;
        }
    }
}

