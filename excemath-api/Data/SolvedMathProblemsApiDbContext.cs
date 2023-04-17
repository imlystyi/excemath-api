#region Usings-частина

using Microsoft.EntityFrameworkCore;
using excemathApi.Models;

#endregion

namespace excemathApi.Data
{
    /// <summary>
    /// Представляє контролер для контексту бази даних <see cref="SolvedMathProblem"/>.
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
        /// <returns>
        /// Набір всіх сутностей у базі даних у контексті класу <see cref="SolvedMathProblem"/> як <see cref="DbSet{TEntity}"/> з елементів класу <see cref="SolvedMathProblem"/>.
        /// </returns>
        public DbSet<SolvedMathProblem> SolvedMathProblems { get; set; }

        #endregion
    }
}
