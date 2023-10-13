using Blog.CoreLayer.Services.Categories;
using Blog.CoreLayer.Services.Comments;
using Blog.CoreLayer.Services.FileManager;
using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Services.Users;
using Blog.DataLayer.Context;
using Blog.CoreLayer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddRazorPages();
services.AddControllersWithViews();

services.AddScoped<IUserService, UserService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddTransient<IPostService, PostService>();
services.AddTransient<IFileManager, FileManager>();
services.AddTransient<ICommentService, CommentService>();
services.AddTransient<IMainPageService, MainPageService>();

services.AddDbContext<BlogContext>(option =>
{
    option.UseSqlServer("Server=.;DataBase=BlogDB;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True");
    option.UseSqlServer(b => b.MigrationsAssembly("Blog-Web"));
});

services.AddAuthorization(option =>
{
    option.AddPolicy("AdminPolicy", builder =>
    {
        builder.RequireRole("Admin");
    });
});

services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/Auth/Login";
    option.LogoutPath = "/Auth/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
    option.AccessDeniedPath = "/";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ErrorHandler/500");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapRazorPages();

app.Run();
