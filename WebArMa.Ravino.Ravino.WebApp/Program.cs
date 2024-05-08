using Application.Contexts;
using Application.Services.BlogCategories;
using Application.Services.Blogs;
using Application.Services.Captchas;
using Application.Services.Contents;
using Application.Services.UploadImages;
using Application.Services.Users;
using Domain.Entities.Users;
using Infrastracture.Configurations.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

#region DbContext

var connection = builder.Configuration["Connections:SQL"];
builder.Services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(connection));

#endregion

#region Identity

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders()
    .AddRoles<ApplicationRole>()
    .AddErrorDescriber<CustomIdentityError>();

#endregion

#region IOT

builder.Services.AddTransient<IDataBaseContext, DataBaseContext>();
builder.Services.AddTransient<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<IContentService, ContentService>();
builder.Services.AddTransient<IUploadImageService, UploadImageService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICaptchaService, CaptchaService>();

builder.Services.AddTransient<IUserHelper, UserHelper>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
