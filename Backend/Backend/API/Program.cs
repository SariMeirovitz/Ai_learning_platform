using BL.Api;
using BL.Services;
using DAL.Api;
using DAL;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Services;
using Microsoft.OpenApi.Models;

// --- הגדרת JWT ---
var builder = WebApplication.CreateBuilder(args);

// הוספת מפתח סודי מהגדרות
var jwtKey = builder.Configuration["Jwt:Key"] ?? "SuperSecretKey123!";

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

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// רישום JwtService
builder.Services.AddSingleton<JwtService>();

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

builder.Services.AddSingleton<OpenAiService>(provider =>
{
    var apiKey = builder.Configuration["OpenAi:ApiKey"];
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("OpenAi:ApiKey configuration is missing or empty.");
    }
    return new OpenAiService(apiKey);
});

// Swagger
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mamash API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

// --- הפעלת Authentication ---
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

