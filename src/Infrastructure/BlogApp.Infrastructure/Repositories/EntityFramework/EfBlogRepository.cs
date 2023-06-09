using System;
using BlogApp.Entities;
using BlogApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
    public class EfBlogRepository : EfEntityRepositoryBase<Blog, BlogAppContext>, IBlogRepository
    {
        public EfBlogRepository(BlogAppContext blogAppContext) : base(blogAppContext)
        {

        }

        public async Task<IEnumerable<Blog?>> GetBlogsByCategoryAsync(int categoryId)
        {
            return await _context.Blogs.AsNoTracking()
                                        .Where(c => c.CategoryId == categoryId)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetBlogsByNameAsync(string title)
        {
            return await _context.Blogs.AsNoTracking().Where(c => c.Title.Contains(title)).ToListAsync();

        }

    }
}

