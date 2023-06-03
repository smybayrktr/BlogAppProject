using System;
using BlogApp.Services;
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

        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.GetCategoriesForList();
            return View(categories);
        }

    }
}

