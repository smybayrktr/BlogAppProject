using System;
using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Services.Extensions;
using BlogApp.Services.Repositories.AppUser;

namespace BlogApp.Services.Repositories.BlogAction
{
    public class SavedBlogService : ISavedBlogService
    {
        private readonly ISavedBlogRepository _savedBlogRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SavedBlogService(ISavedBlogRepository savedBlogRepository, IMapper mapper, IUserService userService)
        {
            _savedBlogRepository = savedBlogRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<SaveBlogResponse> UserSaveAction(CreateSaveBlogRequest createSaveBlogRequest)
        {
            var user = await _userService.GetCurrentUser();
            var saveBlogResponse = new SaveBlogResponse();
            if (user == null)
            {
                saveBlogResponse.Message = Messages.NotAuthorized;
                saveBlogResponse.BookmarkImage = Images.UnsavedBookmark;
                return saveBlogResponse;
            }
            var checkSavedBlog = await _savedBlogRepository.GetWithPredicateAsync(x => x.BlogId == createSaveBlogRequest.BlogId);

            if (checkSavedBlog == null)
            {
                var response = createSaveBlogRequest.ConvertToDto(_mapper, user.Id);
                await _savedBlogRepository.CreateAsync(response);
                saveBlogResponse.BookmarkImage = Images.SavedBookmark;
                saveBlogResponse.Message = Messages.SavedBookmark;
            }
            else
            {
                await _savedBlogRepository.DeleteAsync(checkSavedBlog);
                saveBlogResponse.BookmarkImage = Images.UnsavedBookmark;
                saveBlogResponse.Message = Messages.UnsavedBookmark;
            }
            return saveBlogResponse;
        }
        public async Task<SavedBlog?> GetSavedBlogByBlogIdAsync(int blogId)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            return await _savedBlogRepository.GetWithPredicateAsync (x => x.BlogId == blogId);

        }

        public async Task<IEnumerable<SavedBlog?>> GetSavedBlogsByUserAsync()
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
            {
                return null;
            }
            return await _savedBlogRepository.GetAllWithPredicateAsync(u=>u.UserId == user.Id);
        }
    }
}

