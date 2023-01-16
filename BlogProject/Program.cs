using System.Reflection;
using AutoMapper;
using BlogProject.Database;
using BlogProject.Mapper;
using BlogProject.Models.Database.Users;
using BlogProject.Repositories;
using BlogProject.Repositories.Impl;
using BlogProject.Services;
using BlogProject.Services.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new UserProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

#region Database

builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();

builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 5;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationContext>();

#endregion

#region Services

builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddScoped<IUserService, UserService>();

#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Blog API",
        Description = "Вспомогательная API для работы с сущностями проекта",
        Version = "v1",
        Contact = new OpenApiContact(){ Name = "Eugene Evseenko", Email = "jonikevseenko@gmail.com", Url = new Uri("https://www.evseenko.kz/")}
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // pick comments from classes, including controller summary comments
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true); 
    options.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();