

using Microsoft.EntityFrameworkCore;
using PhotoShare.Api;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.IServices;
using PhotoShare.Data;
using PhotoShare.Data.Repositories;
using PhotoShare.Service.Services;
using PhotoShare.Core;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ITagService,TagService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddAutoMapper(typeof(MappingPostProfile), typeof(MappingProfile));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var connetionString = builder.Configuration.GetConnectionString("PhotoShareContext");  
builder.Services.AddDbContext<PhotoShareContext>(options =>
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString),options=>options.CommandTimeout(60)));


//services.AddAutoMapper(typeof(MappingProfile), typeof(MappingPostProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // ���� ���
    app.UseSwaggerUI(c => // ���� ���
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotoShare API V1");
        c.RoutePrefix = string.Empty; // ��� ���� �-Swagger �-root
    });

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


