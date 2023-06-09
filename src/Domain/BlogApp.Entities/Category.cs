using System;
using System.Security.Principal;

namespace BlogApp.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Blog?> Blogs { get; set; }

    }
}

