using System;
using BlogApp.Infrastructure.Data;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Services;
using BlogApp.Services.Repositories.Auth;
using BlogApp.Services.Repositories.Blog;
using BlogApp.Services.Repositories.Category;
using BlogApp.Services.Repositories.AppUser;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter.Xml;
using BlogApp.Core.Utilities.FileHelper;
using BlogApp.Entities;
using BlogApp.Services.Repositories.BlogAction;
using BlogApp.Core.Utilities.UrlHelper;
using Hangfire;
using Hangfire.SqlServer;
using BlogApp.Core.Utilities.EmailHelper;
using BlogApp.Services.Repositories.Email;
using BlogApp.Core.Utilities.Auth0Helper;

namespace BlogApp.Mvc.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            //uygulama build olmadan önce applicationun kullanacağı nesneleri containere eklememiz lazım.
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogRepository, EfBlogRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, EfUserRepository>();

            services.AddScoped<ISavedBlogService, SavedBlogService>();
            services.AddScoped<ISavedBlogRepository, EfSavedBlogRepository>();

            services.AddScoped<IFileHelper, FileHelper>();
            services.AddScoped<IUrlHelper, UrlHelper>();
            services.AddScoped<IEmailService, EmailService>();
            

            services.AddScoped<IAuthService, AuthService>();
            services.AddDbContext<BlogAppContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("BlogApp")), ServiceLifetime.Transient);
            services.AddDbContext<HangfireContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("Hangfire")), ServiceLifetime.Transient);

            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };
                config.UseSqlServerStorage(configuration.GetConnectionString("Hangfire"), option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });
            services.AddHangfireServer();

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 7 });

            var emailConfig = configuration.GetSection("EmailConfiguration")
                                           .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            var auth0Settings = configuration.GetSection("Auth0")
                                        .Get<Auth0Settings>();
            services.AddSingleton(auth0Settings);

            return services;
        }
    }
}

