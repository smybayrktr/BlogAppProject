using System;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;

namespace BlogApp.Mvc.Models
{
	public class LikedBlog
	{
		public List<LikedBlogItem> LikedBlogs { get; set; } = new List<LikedBlogItem>();

		public void ClearAll() => LikedBlogs.Clear();

        public void AddLikedBlog (LikedBlogItem blog) => LikedBlogs.Add(blog);

    }
    public class LikedBlogItem
    {
        public BlogCardResponse BlogCard { get; set; }

    }
}

