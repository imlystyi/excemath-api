#region Usings-частина

using Microsoft.EntityFrameworkCore;
using excemathApi.Models;

#endregion

namespace excemathApi.Data
{
    public class SolvetMathProblemsApiDbContext: DbContext
    {
        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="MathProblemsApiDbContext"/>, використовуючи базовий конструктор класу <see cref="DbContext"/> та зазначені налаштування.
        /// </summary>
        /// <param name="options">Налаштування контексту.</param>
        public SolvetMathProblemsApiDbContext(DbContextOptions<SolvetMathProblemsApiDbContext> options) : base(options)
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
        public DbSet<SolvetMathProblem> SolvetMathProblems { get; set; }

        #endregion
    }


}
