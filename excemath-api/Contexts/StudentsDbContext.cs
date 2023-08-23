using excemathApi.Models;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Contexts;

/// <summary>
/// Represents a session with the database and be used to query and save entities of the <see cref="StudentDto"/> class.
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
