using eShopSlution.Data.EF;
using eShopSlution.Data.Entities;
using eShopSolution.Utilities.Constants;
using EShopSolution.Application2.Catalog.Products;
using EShopSolution.Application2.Common;
using EShopSolution.Application2.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<eShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MainConectionString)));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<eShopDbContext>().AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger EshopSlution", Version = "v1" })); 
//Declare DI

builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IpublicProductService, PublicProductService>();
builder.Services.AddTransient<ImanagerProductService, ManagerProuctService>();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddTransient<IUserService, UserService>();

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
app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger eShopSlution V1"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
