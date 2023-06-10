using System;
using System.Collections.Generic;
using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.FileHelper;
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
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ISavedBlogService _savedBlogService;
        private readonly IUrlHelper _urlHelper;
        private readonly IFileHelper _fileHelper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper, IUserService userService,
            ISavedBlogService savedBlogService, IUrlHelper urlHelper, IFileHelper fileHelper)
        {
            _blogRepository = blogRepository;
            _userService = userService;
            _mapper = mapper;
            _savedBlogService = savedBlogService;
            _urlHelper = urlHelper;
            _fileHelper = fileHelper;
        }

        public async Task CreateBlogAsync(CreateNewBlogRequest newBlogRequest)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null) return;
            
            var blog = newBlogRequest.ConvertToDto(_mapper, user.Id);
            blog.Url = GenerateUrl(blog.Title);
            await _blogRepository.CreateAsync(blog);
        }

        public async Task<BlogCardResponse?> GetBlogAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(id);
            var response = blog.ConvertToDto(_mapper);
            return response;
        }

        public async Task<UpdateBlogRequest?> GetBlogAsUpdateBlogDtoAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(id);
            var response = blog.ConvertToUpdateDto(_mapper);
            return response;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetBlogsByCategoryAsync(int categoryId)
        {
            var blogs = await _blogRepository.GetBlogDtosByCategory(categoryId);
            var responses = blogs.ConvertToDto(_mapper);
            return responses;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetBlogsCardResponsesAsync()
        {
            var blogCardDtos = await _blogRepository.GetBlogCardDtos();
            var responses = blogCardDtos.ConvertToDto(_mapper);
            return responses;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetBlogsByUserAsync()
        {
            var userCheck = await _userService.GetCurrentUser();
            if (userCheck == null) return null;
            
            var blogs = await _blogRepository.GetAllWithPredicateAsync(x => x.UserId == userCheck.Id);
            var response = blogs.ConvertToDto(_mapper);
            return response;
        }

        public async Task<IEnumerable<BlogCardResponse?>> GetSavedBlogsAsync()
        {
            var userCheck = await _userService.GetCurrentUser();
            if (userCheck == null) return null;
            
            var blogCardDtos = await _blogRepository.GetSavedBlogCardDtos(userCheck.Id);
            var responses = blogCardDtos.ConvertToDto(_mapper);
            return responses;
        }

        private string GenerateUrl(string url)
        {
            return _urlHelper.ToSeoUrl(url) + "-" + Guid.NewGuid();
        }

        public async Task<BlogCardResponse?> GetBlogByUrlAsync(string url)
        {
            var blog = await _blogRepository.GetWithPredicateAsync(u => u.Url == url.Trim());
            if (blog == null) return null;
            
            return blog.ConvertToDto(_mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var blogToDelete = await _blogRepository.GetAsync(id);
            if (blogToDelete == null) return;
            
            await _blogRepository.DeleteAsync(blogToDelete);
        }

        public async Task<bool> UpdateAsync(UpdateBlogRequest updateBlogRequest)
        {
            var currentUser = await _userService.GetCurrentUser();
            if (currentUser == null) return false;   
            
            var blogToUpdate = await _blogRepository.GetAsync(updateBlogRequest.Id);
            if (blogToUpdate==null) return false;
            
            var checkIfBlogBelongsToCurrentUser = currentUser.Id != blogToUpdate.UserId;
            if (checkIfBlogBelongsToCurrentUser) return false;
            
            blogToUpdate.Title = updateBlogRequest.Title;
            blogToUpdate.Body = updateBlogRequest.Body;
            blogToUpdate.CategoryId = updateBlogRequest.CategoryId;
            blogToUpdate.Url = GenerateUrl(updateBlogRequest.Title);

            if (updateBlogRequest.NewImage != null)
                blogToUpdate.Image = await _fileHelper.UploadImage(updateBlogRequest.NewImage);
            
            await _blogRepository.UpdateAsync(blogToUpdate);
            return true;
        }
    }
}

