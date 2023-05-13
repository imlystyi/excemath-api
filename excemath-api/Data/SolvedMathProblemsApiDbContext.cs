using excemathApi.Models;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Data;

/// <summary>
/// Представляє контекст бази даних, який забезпечує зв'язок між об'єктами класу <see cref="SolvedMathProblem"/> і фізичною базою даних.
/// </summary>
public class SolvedMathProblemsApiDbContext : DbContext
{
    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="SolvedMathProblemsApiDbContext"/>, використовуючи базовий конструктор класу <see cref="DbContext"/> та зазначені налаштування.
    /// </summary>
    /// <param name="options">Налаштування контексту.</param>
    public SolvedMathProblemsApiDbContext(DbContextOptions<SolvedMathProblemsApiDbContext> options) : base(options)
    {
    }

    #endregion

    #region Властивості

    /// <summary>
    /// Повертає або встановлює набір всіх сутностей у базі даних у контексті класу <see cref="SolvedMathProblem"/>.
    /// </summary>
    public DbSet<SolvedMathProblem> SolvedMathProblems { get; set; }

    #endregion
}
