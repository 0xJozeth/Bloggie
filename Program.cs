﻿using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore; // This is required for DbContextOptionsBuilder
using Microsoft.EntityFrameworkCore.SqlServer; // This is required for UseSqlServer method


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Inject Db context inside of the services of our application
builder.Services.AddDbContext<BloggieDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

builder.Services.AddScoped<ITagRepository,  TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();



var app = builder.Build();

// Set ASPNETCORE_ENVIRONMENT to Production if not in development
if (!app.Environment.IsDevelopment())
{
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

