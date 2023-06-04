using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Mvc.Extensions;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.Mvc.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var likedBlog = getLikedBlogFromSession();
            return View(likedBlog);
        }

        public IActionResult AddLikeBlogs(int id)
        {
            var selectedBlog = _blogService.GetBlog(id);
            var likedBlogItem = new LikedBlogItem { BlogCard = selectedBlog };
            LikedBlog likedBlog = getLikedBlogFromSession();
            likedBlog.AddLikedBlog(likedBlogItem);
            saveToSession(likedBlog);
            return Json(new { message = $"{selectedBlog.Title} Beğenilenler Listesine Eklendi." });
        }

        private LikedBlog getLikedBlogFromSession()
        {
             return HttpContext.Session.GetJson<LikedBlog>("likedCard") ?? new LikedBlog();
        }


        private void saveToSession(LikedBlog likedBlog)
        {
             HttpContext.Session.SetJson("likedCard", likedBlog);
        }
    }
}

