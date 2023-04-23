using Microsoft.EntityFrameworkCore;
using excemathApi.Models;

namespace excemathApi.Data;

/// <summary>
/// Представляє контекст бази даних, який забезпечує зв'язок між класом моделі математичної проблеми <see cref="MathProblem"/> і фізичною базою даних.
/// </summary>
public class MathProblemsApiDbContext : DbContext
{
    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="MathProblemsApiDbContext"/>, використовуючи базовий конструктор класу <see cref="DbContext"/> та зазначені налаштування.
    /// </summary>
    /// <param name="options">Налаштування контексту.</param>
    public MathProblemsApiDbContext(DbContextOptions<MathProblemsApiDbContext> options) : base(options)
    {
    }

    #endregion

    #region Властивості

    /// <summary>
    /// Повертає або встановлює набір всіх сутностей у базі даних у контексті класу <see cref="MathProblem"/>.
    /// </summary>
    /// <returns>
    /// Набір всіх сутностей у базі даних у контексті класу <see cref="MathProblem"/> як <see cref="DbSet{TEntity}"/> з елементів класу <see cref="MathProblem"/>.
    /// </returns>
    public DbSet<MathProblem> MathProblems { get; set; }

    #endregion
}
