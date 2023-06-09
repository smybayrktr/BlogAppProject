﻿using System;
using AutoMapper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApp.Services.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //Neyi-neye dönüştüreceğimizi yazdık.
            CreateMap<Blog, BlogCardResponse>();
            CreateMap<Category, CategoryDisplayResponse>();
            CreateMap<CreateNewBlogRequest, Blog>();
            CreateMap<UserRegisterRequest, User>();
            CreateMap<UserLoginRequest, User>();
            CreateMap<CreateSaveBlogRequest, SavedBlog>();
            CreateMap<BlogCardDto, BlogCardResponse>();
            CreateMap<UpdateBlogRequest, Blog>();
            CreateMap<Blog, UpdateBlogRequest>();
        }
    }
}

