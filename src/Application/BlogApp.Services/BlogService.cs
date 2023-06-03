using System;
using AutoMapper;
using BlogApp.DataTransferObjects.Responses;
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

