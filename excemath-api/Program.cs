// excemath API - open source API for educational projects related to mathematics
// Copyright (C) 2023  miu-miu enjoyers
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Contact us:
// i.   By paper mail: 23 Yevhena Patona street, Zaliznychnyi raion, Lviv, Lviv oblast, 79040, Ukraine
// ii.  By email: vladyslav.yakubovskyi.work@gmail.com
//
// See the official repository page on GitHub: <https://github.com/miu-miu-enjoyers/excemath-api>

using excemathApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace excemathApi;

/// <summary>
/// Represents the entry point of the program.
/// </summary>
public static class Program
{
    private const string _CONNECTION_STRING_NAME = "";

    /// <summary>
    /// Builds a program.
    /// </summary>
    /// <param name="args">Line arguments.</param>
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
