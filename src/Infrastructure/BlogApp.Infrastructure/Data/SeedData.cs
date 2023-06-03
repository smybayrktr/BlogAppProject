using System;
using BlogApp.Entities;

namespace BlogApp.Infrastructure.Data
{
       public static class SeedData
        {
            public static void SeedDatabase(BlogAppContext context)
            {
                seedUserIfNotExists(context);
                seedCategoryIfNotExists(context);
                seedBlogIfNoExists(context);
            }

            private static void seedCategoryIfNotExists(BlogAppContext context)
            {
                if (!context.Categories.Any())
                {
                    var categories = new List<Category>()
                {
                    new(){Name=".Net" },
                    new(){Name="Java" },

                };
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }
            }
            private static void seedBlogIfNoExists(BlogAppContext context)
            {
                if (!context.Blogs.Any())
                {
                    var blogs = new List<Blog>() {
                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı1?",
                                                           Body="..",
                                                           Image="https://loremflickr.com/320/240",
                                                           CategoryId=1,
                                                           Url=".net",
                                                           UserId=1 },

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı2?",
                                                         Body="..",
                                                         Image="https://loremflickr.com/320/240",
                                                         CategoryId=1,
                                                         Url=".",
                                                         UserId=1 },

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı3?",
                                                        Body="..",
                                                        Image="https://loremflickr.com/320/240",
                                                        CategoryId=2,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı4?",
                                                        Body="..",
                                                        Image="https://loremflickr.com/320/240",
                                                        CategoryId=1,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı5?",
                                                        Body="..",
                                                        Image="https://loremflickr.com/320/240",
                                                        CategoryId=2,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı6?",
                                                        Body="..",
                                                        Image="https://loremflickr.com/320/240",
                                                        CategoryId=1,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı7?",
                                                        Body="..",
                                                        Image="https://loremflickr.com/320/240",
                                                        CategoryId=2,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı8?",
                                                        Body="..",
                                                        Image="https://loremflickr.com/320/240",
                                                        CategoryId=1,
                                                        Url=".net",
                                                        UserId=1},
                };

                    context.Blogs.AddRange(blogs);
                    context.SaveChanges();
                }

            }
            private static void seedUserIfNotExists(BlogAppContext context)
            {
                if (!context.Users.Any())
                {
                    var users = new List<User>()
                {
                    new(){ Email="admin@gmail.com",
                           UserName="admin@gmail.com",
                           SecurityStamp=Guid.NewGuid().ToString()},
                };
                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
            }

        
    }
}

