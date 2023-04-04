#region Usings-частина
using Microsoft.EntityFrameworkCore;
using excemathApi.Models;
#endregion

namespace excemathApi.Data
{
    /// <summary>
    /// Надає контекст класу <see cref="MathProblem"/> для API та бази даних.
    /// </summary>
    public class MathProblemsAPIDbContext : DbContext
    {
        #region Конструктори
        /// <summary>
        /// Створює екземпляр класу, використовуючи базовий конструктор класу <see cref="DbContext"/> та зазначені налаштування.
        /// </summary>
        /// <param name="options">Налаштування контексту.</param>
        public MathProblemsAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region Властивості
        /// <summary>
        /// Повертає або встановлює колекцію всіх сутностей у контексті класу <see cref="MathProblem"/>.
        /// </summary>
        /// <returns>
        /// Колекцію всіх сутностей у контексті класу <see cref="MathProblem"/>.
        /// </returns>
        public DbSet<MathProblem> MathProblems { get; set; }
        #endregion
    }
}
