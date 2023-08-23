using excemathApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace excemathApi;

// TODO: Program class documentation.

public static class Program
{
    private const string _CONNECTION_STRING_NAME = "excemathDb";

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration.GetConnectionString(_CONNECTION_STRING_NAME);

        _ = builder.Services.AddControllers().AddNewtonsoftJson();
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen();

        _ = builder.Services.AddDbContext<MathProblemsDbContext>(options => options.UseNpgsql(connectionString));
        _ = builder.Services.AddDbContext<StudentsDbContext>(options => options.UseNpgsql(connectionString));

        _ = builder.Services.AddAuthorization();

        WebApplication app = builder.Build();

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