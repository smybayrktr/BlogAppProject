using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Utilities.FileHelper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Mvc.Extensions;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using BlogApp.Services.Repositories.Blog;
using BlogApp.Services.Repositories.BlogAction;
using BlogApp.Services.Repositories.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IUrlHelper = BlogApp.Core.Utilities.UrlHelper.IUrlHelper;


namespace BlogApp.Mvc.Controllers
{

    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IFileHelper _fileHelper;
        private readonly ISavedBlogService _savedBlogService;
        private readonly IUrlHelper _urlHelper;

        public BlogsController(IBlogService blogService, ICategoryService categoryService, IFileHelper fileHelper, ISavedBlogService savedBlogService, IUrlHelper urlHelper)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _savedBlogService = savedBlogService;
            _fileHelper = fileHelper;
            _urlHelper = urlHelper;
        }
        [HttpGet("/blogs")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/get-user-blogs")]
        public async Task<IActionResult?> GetBlogsByUser()
        {
            var result = await _blogService.GetBlogsByUserAsync();
            if (result == null)
                return RedirectToAction("AccessDenied", "Auth");
            return View(result);
        }


        [HttpPost("/save-blog")]
        public async Task<SaveBlogResponse> UserSaveAction(CreateSaveBlogRequest createSaveBlogRequest)
        {
            return await _savedBlogService.UserSaveAction(createSaveBlogRequest);
        }

        [HttpGet("/get-saved-blogs")]
        public async Task<IActionResult> GetSavedBlogs()
        {
            var savedBlogs = await _blogService.GetSavedBlogsAsync();
            return View(savedBlogs);
        }

        [HttpGet("/create-blog")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await getCategoriesForSelectList();
            return View();
        }

        [HttpPost("/create-blog")]
        public async Task<IActionResult> Create(CreateNewBlogRequestViewModel createNewBlogRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                var createNewBlogRequest = new CreateNewBlogRequest()
                {
                    Title = createNewBlogRequestViewModel.Title,
                    Body = createNewBlogRequestViewModel.Body,
                    CategoryId = createNewBlogRequestViewModel.CategoryId,
                    Image = await _fileHelper.UploadImage(createNewBlogRequestViewModel.Image)
                };

                await _blogService.CreateBlogAsync(createNewBlogRequest);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = await getCategoriesForSelectList();
            return View();
        }

        [HttpGet("/blog/{url}")]
        public async Task<IActionResult> BlogDetail(string url)
        {
            var blog = await _blogService.GetBlogByUrlAsync(url);
            if (blog == null)
                return RedirectToAction("Index", "Home");
            return View(blog);
        }

        [HttpPost("/upload-blog-image")]
        public async Task<string> Upload(IFormFile file)
        {
            if (file == null) return null;
            var url = await _fileHelper.UploadImage(file);
            return _urlHelper.AddBaseUrlToUrl(url);
        }

        [Route("/delete-blog")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction("GetBlogsByUser", "Blogs");
        }

        [HttpGet("/update-blog")]
        public async Task<IActionResult> Update(int id)
        {
            var getBlog = await _blogService.GetBlogAsUpdateBlogDtoAsync(id);
            if (getBlog==null)
                return RedirectToAction("Index","Home");
            ViewBag.Categories = await getCategoriesForSelectList();
            return View(getBlog);
        }

        [HttpPost("/update-blog")]
        public async Task<IActionResult> Update(UpdateBlogRequest updateBlogRequest)
        {
            var updateResult = await _blogService.UpdateAsync(updateBlogRequest);
            if (!updateResult)
                return RedirectToAction("AccessDenied", "Auth");
            return RedirectToAction("GetBlogsByUser","Blogs");
        }

        private async Task<IEnumerable<SelectListItem?>> getCategoriesForSelectList()
        {
            var categories = (await _categoryService.GetCategoriesForListAsync()).Select(c => new SelectListItem
            { Text = c.Name, Value = c.Id.ToString() }).ToList();
            return categories;
        }
    }
}

