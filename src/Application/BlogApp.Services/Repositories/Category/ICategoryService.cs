using System;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services.Repositories.Category
{
	public interface ICategoryService
	{
        Task<IEnumerable<CategoryDisplayResponse?>> GetCategoriesForListAsync();

    }
}

