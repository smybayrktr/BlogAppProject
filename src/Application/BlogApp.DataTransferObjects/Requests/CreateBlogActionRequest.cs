using System;
namespace BlogApp.DataTransferObjects.Requests
{
	public class CreateBlogActionRequest
	{
        public int UserId { get; set; }

        public int BlogId { get; set; }
    }
}

