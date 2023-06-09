using System;
using BlogApp.Entities;

namespace BlogApp.Infrastructure.Repositories
{
    public interface IBlogRepository : IEntityRepository<Blog>
    {
        Task<IEnumerable<Blog?>> GetBlogsByCategoryAsync(int categoryId);

        Task<IEnumerable<Blog>> GetBlogsByNameAsync(string title);

    }
}

