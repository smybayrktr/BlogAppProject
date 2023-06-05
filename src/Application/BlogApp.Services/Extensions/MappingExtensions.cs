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

        public static BlogAction ConvertToDto(this CreateBlogActionRequest createBlogActionRequest, IMapper mapper) =>
             mapper.Map<BlogAction>(createBlogActionRequest);

        public static Blog ConvertToDto(this CreateNewBlogRequest newBlogRequest, IMapper mapper) =>
             mapper.Map<Blog>(newBlogRequest);

    }
}

