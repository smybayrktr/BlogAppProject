using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Mvc.Extensions;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.Mvc.Controllers
{
   
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public BlogsController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult GetLikeBlogs(int id)
        {
            var likedBlog = getLikedBlogFromSession();
            return View(likedBlog);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = getCategoriesForSelectList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateNewBlogRequest createNewBlogRequest)
        {
            if (ModelState.IsValid)
            {
                await _blogService.CreateBlogAsync(createNewBlogRequest);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = getCategoriesForSelectList();
            return View();
        }

        private LikedBlog getLikedBlogFromSession()
        {
            return HttpContext.Session.GetJson<LikedBlog>("likedCard") ?? new LikedBlog();
        }


        private void saveToSession(LikedBlog likedBlog)
        {
            HttpContext.Session.SetJson("likedCard", likedBlog);
        }

        private IEnumerable<SelectListItem> getCategoriesForSelectList()
        {
            var categories = _categoryService.GetCategoriesForList().Select(c => new SelectListItem
                                                                    { Text = c.Name, Value = c.Id.ToString() })
                                                                    .ToList();
            return categories;
        }
    }
}

