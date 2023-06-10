using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Mvc.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Authorization;
using BlogApp.Services.Repositories.Blog;

namespace BlogApp.Mvc.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IBlogService _blogService;

    public HomeController(ILogger<HomeController> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    [HttpGet("/home")]
    [Route("")]
    public async Task<IActionResult> Index(int pageNo = 1, int? id = null)
    {
        var blogs = id == null ? await _blogService.GetBlogsCardResponsesAsync()
                               : await _blogService.GetBlogsByCategoryAsync(id.Value);

        var blogPerPage = 4;
        var blogCount = blogs.Count();
        var totalPage = Math.Ceiling((decimal)blogCount / blogPerPage);

        var pagingInfo = new PagingInfo()
        {
            CurrentPage = pageNo,
            ItemsPerPage = blogPerPage,
            TotalItems = blogCount,
            CategoryId = id
        };

        var paginatedBlogs = blogs.OrderBy(b => b.Id)
                                 .Skip((pageNo - 1) * blogPerPage)
                                 .Take(blogPerPage)
                                 .ToList();

        var model = new PaginationBlogViewModel()
        {
            Blogs = paginatedBlogs,
            PagingInfo = pagingInfo,
        };

        return View(model);
    }

    [HttpGet("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

