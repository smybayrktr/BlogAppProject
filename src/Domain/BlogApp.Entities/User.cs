using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public bool Status { get; set; } = true;

        public ICollection<Blog> Blogs { get; set; }
    }
}

