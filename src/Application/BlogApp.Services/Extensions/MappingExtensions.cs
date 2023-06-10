using System;
using AutoMapper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;

namespace BlogApp.Services.Extensions
{
    public static class MappingExtensions
    {

        public static IEnumerable<BlogCardResponse> ConvertToDto(this IEnumerable<Blog> blogs, IMapper mapper) =>
            mapper.Map<IEnumerable<BlogCardResponse>>(blogs);

        public static BlogCardResponse ConvertToDto(this Blog blog, IMapper mapper) =>
             mapper.Map<BlogCardResponse>(blog);

        public static IEnumerable<CategoryDisplayResponse> ConvertToDto(this IEnumerable<Category> categories, IMapper mapper) =>
             mapper.Map<IEnumerable<CategoryDisplayResponse>>(categories);

        public static Blog ConvertToDto(this CreateNewBlogRequest newBlogRequest, IMapper mapper, int userId)
        {
            var blog = mapper.Map<Blog>(newBlogRequest);
            blog.UserId = userId;
            return blog;
        }

        public static User ConvertToDto(this UserRegisterRequest userRegisterRequest, IMapper mapper) =>
             mapper.Map<User>(userRegisterRequest);

        public static User ConvertToDto(this UserLoginRequest userLoginRequest, IMapper mapper) =>
            mapper.Map<User>(userLoginRequest);

        public static IEnumerable<BlogCardResponse> ConvertToDto(this IEnumerable<BlogCardDto> blogCardDtos, IMapper _mapper) => _mapper.Map<IEnumerable<BlogCardResponse>>(blogCardDtos);

        public static SavedBlog ConvertToDto(this CreateSaveBlogRequest createSaveBlogRequest, IMapper mapper, int userId)
        {
            var savedBlog = mapper.Map<SavedBlog>(createSaveBlogRequest);
            savedBlog.UserId = userId;
            return savedBlog;
        }

        public static Blog ConvertToDto(this UpdateBlogRequest updateBlogRequest, IMapper mapper) =>
           mapper.Map<Blog>(updateBlogRequest);

        public static UpdateBlogRequest ConvertToUpdateDto(this Blog blog, IMapper mapper) =>
           mapper.Map<UpdateBlogRequest>(blog);
    }
}

