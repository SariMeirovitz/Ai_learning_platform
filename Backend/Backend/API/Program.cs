using BL.Api;
using BL.Services;
using DAL.Api;
using DAL;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// רישום DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\משתמש\\Desktop\\שרי מסלול\\פרויקט פרקטיקום\\Backend\\Backend\\DAL\\database\\PlatformDB.mdf\";Integrated Security=True;Connect Timeout=30"));

// רישום שירותי DAL
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ISubCategory, SubCategoryService>();
builder.Services.AddScoped<IPrompt, PromptService>();

// DalManager
builder.Services.AddScoped<IDal, DalManager>();

// רישום שירותי BL
builder.Services.AddScoped<IUserBl, UserBl>();
builder.Services.AddScoped<ICategoryBl, CategoryBl>();
builder.Services.AddScoped<ISubCategoryBl, SubCategoryBl>();
builder.Services.AddScoped<IPromptBl, PromptBl>();

// BlManager
builder.Services.AddScoped<IBl, BlManager>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
