using System;
using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.UrlHelper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Services.Extensions;
using BlogApp.Services.Repositories.AppUser;
using BlogApp.Services.Repositories.BlogAction;

namespace BlogApp.Services.Repositories.Blog
{
	public class BlogService: IBlogService
	{
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ISavedBlogService _savedBlogService;
        private readonly IUrlHelper _urlHelper;


        public BlogService(IBlogRepository blogRepository, IMapper mapper, IUserService userService,
            ISavedBlogService savedBlogService, IUrlHelper urlHelper)
        {
            _blogRepository = blogRepository;
            _userService = userService;
            _mapper = mapper;
            _savedBlogService = savedBlogService;
            _urlHelper = urlHelper;
        }

        public async Task CreateBlogAsync(CreateNewBlogRequest newBlogRequest)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
            {
                return;
            }
            var blog = newBlogRequest.ConvertToDto(_mapper, user.Id);
            blog.Url = GenerateUrl(blog.Title);
            await _blogRepository.CreateAsync(blog);
        }

        public async Task<BlogCardResponse?> GetBlogAsync(int id)
        {
            var blog= await _blogRepository.GetAsync(id);
            var response = blog.ConvertToDto(_mapper);
            return response;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetBlogsByCategoryAsync(int categoryId)
        {
            var blogs = await _blogRepository.GetBlogsByCategoryAsync(categoryId);
            var responses = blogs.ConvertToDto(_mapper);
            foreach (var response in responses)
            {
                response.BookmarkImage = await _savedBlogService.GetSavedBlogByBlogIdAsync(response.Id) == null
                                       ? Images.UnsavedBookmark
                                       : Images.SavedBookmark;
            }
            return responses;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetBlogsCardResponsesAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();
            var responses = blogs.ConvertToDto(_mapper);
            foreach (var response in responses)
            {
                response.BookmarkImage = await _savedBlogService.GetSavedBlogByBlogIdAsync(response.Id) == null
                                       ? Images.UnsavedBookmark
                                       : Images.SavedBookmark;
            }
            return responses;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetBlogsByUserAsync()
        {
            var userCheck = await _userService.GetCurrentUser();
            if (userCheck == null)
            {
                return null;
            }
            var blogs = await _blogRepository.GetAllWithPredicateAsync(x=>x.UserId == userCheck.Id);
            var response = blogs.ConvertToDto(_mapper);
            return response;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetSavedBlogs()
        {
            var savedBlogs = await _savedBlogService.GetSavedBlogsByUserAsync();
            var blogCardResponses = new List<BlogCardResponse>();
            foreach (var savedBlog in savedBlogs)
            {
                var blog = await _blogRepository.GetAsync(savedBlog.BlogId);
                var blogCardResponse = blog.ConvertToDto(_mapper);
                blogCardResponse.BookmarkImage = Images.SavedBookmark;
                blogCardResponses.Add(blogCardResponse);
            }
            return blogCardResponses;
        }

        private string GenerateUrl(string url)
        {
            return _urlHelper.ToSeoUrl(url) + "-" + Guid.NewGuid();
        }

        public async Task<BlogCardResponse?> GetBlogByUrl(string url)
        {
            var blog = await _blogRepository.GetWithPredicateAsync(u=>u.Url == url.Trim());
            if (blog == null)
            {
                return null;
            }
            return blog.ConvertToDto(_mapper);
        }
    }
}

