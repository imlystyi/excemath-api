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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddDbContext<MathProblemsAPIDbContext>(options => options.UseInMemoryDatabase("MathProblemsDb"));
            builder.Services.AddDbContext<MathProblemsApiDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddDbContext<UsersApiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();

            // Конфігурація системи одержання та відправки запитів HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}