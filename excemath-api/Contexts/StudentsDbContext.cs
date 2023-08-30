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

using excemathApi.Models;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Contexts;

/// <summary>
/// Represents a session with the database and can be used to query and save entities of the <see cref="StudentDto"/> type.
/// </summary>
public class StudentsDbContext : DbContext
{
    /// <summary>
    /// Gets or sets a database set of the <see cref="StudentDto"/> class objects.
    /// </summary>
    public DbSet<StudentDto> Students { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentsDbContext"/> class, using the base <see cref="DbContext"/> class constructor and specified options.
    /// </summary>
    /// <param name="options">Context options.</param>
    public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options) { }
}
