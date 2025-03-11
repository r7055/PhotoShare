
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

using Microsoft.EntityFrameworkCore;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.IServices;
using PhotoShare.Data;
using PhotoShare.Data.Repositories;
using PhotoShare.Service.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PhotoShareContext>(options =>
    options.UseMySQL("server=localhost;database=photo_share_db;user=root;password=aA1795aA"));



// Add services to the container.
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();//??
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
