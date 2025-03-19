using Microsoft.EntityFrameworkCore;
using PhotoShare.Api;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.IServices;
using PhotoShare.Data;
using PhotoShare.Data.Repositories;
using PhotoShare.Service.Services;
using PhotoShare.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddAutoMapper(typeof(MappingPostProfile), typeof(MappingProfile));

builder.Services.AddControllers();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddSingleton<IAmazonS3>(serviceProvider =>
{
    var options = serviceProvider.GetRequiredService<IOptions<AWSOptions>>().Value;

    // ����� Credentials ����� ����
    var credentials = new Amazon.Runtime.BasicAWSCredentials(
        builder.Configuration["AWS:AccessKey"],
        builder.Configuration["AWS:SecretKey"]
    );

    // ����� Region
    var region = Amazon.RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"]);

    return new AmazonS3Client(credentials, region);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PhotoShareContext");
builder.Services.AddDbContext<PhotoShareContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.CommandTimeout(60)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add role-based authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("EditorOrAdmin", policy => policy.RequireRole("Editor", "Admin"));
    // options.AddPolicy("ViewerOnly", policy => policy.RequireRole("Viewer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotoShare API V1");
        c.RoutePrefix = string.Empty; // To access Swagger at the root
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
