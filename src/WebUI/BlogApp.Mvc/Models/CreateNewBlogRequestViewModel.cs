using System;
namespace BlogApp.Mvc.Models
{
	public class CreateNewBlogRequestViewModel
	{
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}

