using excemathApi.Models;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Contexts;

/// <summary>
/// Represents a session with the database and can be used to query and save entities of the <see cref="MathProblemDto"/> type.
/// </summary>
public class MathProblemsDbContext : DbContext
{
    /// <summary>
    /// Gets or sets a database set of the <see cref="MathProblemDto"/> class objects.
    /// </summary>
    public DbSet<MathProblemDto> MathProblems { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathProblemsDbContext"/> class, using the base <see cref="DbContext"/> class constructor and specified options.
    /// </summary>
    /// <param name="options">Context options.</param>
    public MathProblemsDbContext(DbContextOptions<MathProblemsDbContext> options) : base(options) { }
}
