using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Repositories;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ────── Database ──────
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.UseSqlite("Data Source=smart_library.db");
    }
    else if (connStr != null && (connStr.Contains("Host=") || connStr.Contains("Port=") || connStr.Contains("postgres://")))
    {
        if (connStr.StartsWith("postgres://"))
        {
            connStr = ParsePostgresUrl(connStr);
        }
        options.UseNpgsql(connStr);
    }
    else
    {
        options.UseSqlServer(connStr);
    }
});

// ────── Repositories (Repository Pattern) ──────
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// ────── Services (DI) ──────
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BorrowService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<UserAdminService>();
builder.Services.AddScoped<ReservationService>();

// ────── JWT Authentication ──────
var jwt = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwt["SecretKey"]!);

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
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// ────── CORS (allow Angular dev server) ──────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ────── Controllers ──────
builder.Services.AddControllers();

// ────── Swagger / OpenAPI ──────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Smart Campus Library API",
        Version = "v1",
        Description = "RESTful API for Smart Campus Library Management System"
    });

    // Add JWT auth support to Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ────── Auto-apply migrations & seed on startup ──────
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Warning] Failed to auto-apply migrations on startup: {ex.Message}");
    }
}

// ────── Middleware ──────
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Library API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

string ParsePostgresUrl(string url)
{
    var uri = new Uri(url);
    var userInfo = uri.UserInfo.Split(':');
    var username = userInfo[0];
    var password = userInfo.Length > 1 ? userInfo[1] : string.Empty;
    var host = uri.Host;
    var port = uri.Port;
    var database = uri.AbsolutePath.TrimStart('/');
    return $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=True;";
}
