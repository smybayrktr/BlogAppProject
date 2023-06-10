using System;
namespace BlogApp.Mvc.Models
{
	public class PagingInfo
	{
		public int TotalItems { get; set; }

		public int ItemsPerPage { get; set; }

		public int CurrentPage { get; set; }
		public int? CategoryId { get; set; }

		public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

    }
}

