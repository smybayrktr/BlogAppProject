using System;
using BlogApp.Entities;

namespace BlogApp.Infrastructure.Repositories
{
    public interface IBlogRepository : IEntityRepository<Blog>
    {
        IEnumerable<Blog> GetBlogsByCategory(int categoryId);

        IEnumerable<Blog> GetBlogsByName(string title);
    }
}

