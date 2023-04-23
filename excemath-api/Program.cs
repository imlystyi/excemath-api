using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace excemathApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Додаємо сервіси до контейнеру.
        _ = builder.Services.AddControllers();
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen();
        _ = builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });

        // Налаштування контекстів.
        _ = builder.Services.AddDbContext<MathProblemsApiDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        _ = builder.Services.AddDbContext<UsersApiDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        _ = builder.Services.AddDbContext<SolvedMathProblemsApiDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        _ = builder.Services.AddAuthorization();

        var app = builder.Build();

        // Налаштування системи одержання та відправки запитів HTTP.
        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseHttpsRedirection();
        _ = app.UseAuthentication();
        _ = app.UseAuthorization();
        _ = app.MapControllers();

        app.Run();
    }
}