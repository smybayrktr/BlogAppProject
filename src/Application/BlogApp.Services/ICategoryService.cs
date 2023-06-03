using System;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services
{
	public interface ICategoryService
	{
        public IEnumerable<CategoryDisplayResponse> GetCategoriesForList();

    }
}

