using System;
using System.Security.Principal;
using BlogApp.Core.Utilities.DateHelper;

namespace BlogApp.Entities
{
    public class Blog : IEntity
    {
        public int Id { get; set; }
        public long CreatedAt { get; set; } = DateHelper.DateToTimestampt(DateTime.Now);
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; } = true;
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public User CreatedUser { get; set; }
        public int UserId { get; set; }


    }
}

