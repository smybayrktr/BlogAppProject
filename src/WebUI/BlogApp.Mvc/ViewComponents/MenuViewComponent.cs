using System;
using BlogApp.Services;
using BlogApp.Services.Repositories.Category;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Mvc.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public MenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetCategoriesForListAsync();
            return View(categories);
        }
       
    }
}

