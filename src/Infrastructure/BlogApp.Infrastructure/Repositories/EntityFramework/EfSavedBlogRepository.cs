using System;
using BlogApp.Entities;
using BlogApp.Infrastructure.Data;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
    public class EfBlogActionRepository : EfEntityRepositoryBase<BlogAction, BlogAppContext>, IBlogActionRepository
    {
        public EfBlogActionRepository(BlogAppContext context) : base(context)
        {
        }
    }
}

