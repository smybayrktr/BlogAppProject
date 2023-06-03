using System;
using AutoMapper;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;

namespace BlogApp.Services.Extensions
{
    public static class MappingExtensions
    {
        
        public static IEnumerable<BlogCardResponse> ConvertToDto(this IEnumerable<Blog> blogs, IMapper mapper) =>
            mapper.Map<IEnumerable<BlogCardResponse>>(blogs);

        public static IEnumerable<CategoryDisplayResponse> ConvertToDto(this IEnumerable<Category> categories, IMapper mapper) =>
             mapper.Map<IEnumerable<CategoryDisplayResponse>>(categories);
        


    }
}

