using Microsoft.EntityFrameworkCore;
using excemathApi.Data;

namespace excemathApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Додаємо сервіси до контейнеру.
            _ = builder.Services.AddControllers();
            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen();

            // Налаштування контекстів.
            _ = builder.Services.AddDbContext<MathProblemsApiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            _ = builder.Services.AddDbContext<UsersApiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            _ = builder.Services.AddDbContext<SolvedMathProblemsApiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();

            // Налаштування системи одержання та відправки запитів HTTP.
            if (app.Environment.IsDevelopment())
            {
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI();
            }

            _ = app.UseHttpsRedirection();
            _ = app.UseAuthorization();
            _ = app.MapControllers();

            app.Run();
        }
    }
}