using System;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Mvc.Models
{
	public class PaginationBlogViewModel
	{
        public IEnumerable<BlogCardResponse> Blogs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

