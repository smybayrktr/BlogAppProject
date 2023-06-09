using System;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services.Repositories.CategoryServiceRepository
{
	public interface ICategoryService
	{
        public IEnumerable<CategoryDisplayResponse> GetCategoriesForList();

    }
}

