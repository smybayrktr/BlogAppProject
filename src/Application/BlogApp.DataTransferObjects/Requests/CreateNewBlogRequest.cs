using System;
using BlogApp.Core.Utilities;

namespace BlogApp.DataTransferObjects.Requests
{
	public class CreateNewBlogRequest
	{
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}

