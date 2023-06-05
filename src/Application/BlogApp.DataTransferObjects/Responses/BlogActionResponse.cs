using System;
namespace BlogApp.DataTransferObjects.Responses
{
	public class BlogActionResponse
	{
		public string Message { get; set; }

		//False-Kullanıcı login değil. True-kullanıcı Login Beğenmiştir
		public bool Status { get; set; }
	}
}

