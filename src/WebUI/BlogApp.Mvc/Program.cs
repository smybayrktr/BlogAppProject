using BlogApp.Entities;
using BlogApp.Infrastructure.Data;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Mvc.Extensions;
using BlogApp.Services;
using BlogApp.Services.Mappings;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation() ;


builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddIdentity<User, IdentityRole<int>>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<BlogAppContext>()
    .AddDefaultTokenProviders();

builder.Services.AddInjections(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied"; //Üye ama yetkisi yok
                    options.ReturnUrlParameter = "returnUrl";
                });
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<BlogAppContext>();
context.Database.EnsureCreated();
await SeedData.SeedDatabase(context, scope.ServiceProvider);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Blog App Hangfire",
    AppPath = "/hangfire",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            User = builder.Configuration.GetSection("HangfireSettings:User").Value,
            Pass = builder.Configuration.GetSection("HangfireSettings:Password").Value
        }
    },
    IgnoreAntiforgeryToken = true
});


GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 7 });

//RecurringJobs.DatabaseBackupOperation();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

