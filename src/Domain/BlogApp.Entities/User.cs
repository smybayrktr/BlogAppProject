using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool Status { get; set; } = true;

        public string Role { get; set; }


        public ICollection<Blog> Blogs { get; set; }
    }
}

