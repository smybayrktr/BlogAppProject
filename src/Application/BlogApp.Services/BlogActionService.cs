using System;
using AutoMapper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;
using BlogApp.Entities.Enums;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Services.Extensions;

namespace BlogApp.Services
{
	public class BlogActionService:IBlogActionService
	{
        private readonly IBlogActionRepository _blogActionRepository;
        private readonly IMapper _mapper;

        public BlogActionService(IBlogActionRepository blogActionRepository, IMapper mapper)
        {
            _blogActionRepository = blogActionRepository;
            _mapper = mapper;
        }

        public async Task<BlogActionResponse> Dislike(CreateBlogActionRequest createBlogActionRequest)
        {
            var checkLike = await _blogActionRepository.GetWithPredicateAsync(x => x.UserId == createBlogActionRequest.UserId
                                                                         && x.BlogId == createBlogActionRequest.BlogId);
            if (checkLike == null)
            {
                var response = createBlogActionRequest.ConvertToDto(_mapper);
                response.BlogActionType = BlogActionType.Dislike;
                await _blogActionRepository.CreateAsync(response);
                return new BlogActionResponse() { Message = "Beğenilmedi.", Status = true };
            }

            else
            {
                var response = createBlogActionRequest.ConvertToDto(_mapper);
                await _blogActionRepository.DeleteAsync(response);
                return new BlogActionResponse() { Message = "Beğenmekten vezgeçildi.", Status = false };
            }
        }

        public async Task<BlogActionResponse> Like(CreateBlogActionRequest createBlogActionRequest)
        {
            var checkLike = await _blogActionRepository.GetWithPredicateAsync(x=>x.UserId ==createBlogActionRequest.UserId
                                                                         && x.BlogId==createBlogActionRequest.BlogId);
            if (checkLike == null)
            {
                var response = createBlogActionRequest.ConvertToDto(_mapper);
                response.BlogActionType = BlogActionType.Like;
                await _blogActionRepository.CreateAsync(response);
                return new BlogActionResponse() { Message="Beğenildi.", Status = true};
            }

            else
            {
                var response = createBlogActionRequest.ConvertToDto(_mapper);
                await _blogActionRepository.DeleteAsync(response);
                return new BlogActionResponse() { Message = "Beğenmekten vezgeçildi.", Status = false };
            }

        }
    }
}

