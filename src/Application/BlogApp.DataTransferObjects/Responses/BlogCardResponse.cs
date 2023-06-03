using System;
namespace BlogApp.DataTransferObjects.Responses
{
	public class BlogCardResponse
	{
        public int Id { get; set; }
        public string CreatedAt { get; set; } 
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; } = "https://loremflickr.com/320/240";
        //public Category Category { get; set; }
    }
}

