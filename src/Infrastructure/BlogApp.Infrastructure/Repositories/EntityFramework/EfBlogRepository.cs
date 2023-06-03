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

        public IEnumerable<Blog> GetBlogsByCategory(int categoryId)
        {
            return _context.Blogs.AsNoTracking().Where(c => c.CategoryId == categoryId).AsEnumerable();
        }

        public IEnumerable<Blog> GetBlogsByName(string title)
        {
            return _context.Blogs.AsNoTracking().Where(c => c.Title.Contains(title)).AsEnumerable();

        }

    }
}

