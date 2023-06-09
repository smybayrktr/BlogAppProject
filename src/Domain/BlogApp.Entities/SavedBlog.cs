using System;
using BlogApp.Entities.Enums;

namespace BlogApp.Entities
{
	public class BlogAction:IEntity
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public int BlogId { get; set; }

		public BlogActionType BlogActionType { get; set; }
	}
}

