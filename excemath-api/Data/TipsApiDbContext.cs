// Зробити окрему базу даних для загальних підказок, чи зберігати їх як поля математичних проблем?
// Подумати над цим; моделі, контролери, контексти для Tip поки що не робити!

#region Usings-частина

using Microsoft.EntityFrameworkCore;
using excemathApi.Models;

#endregion

namespace excemathApi.Data
{
    /// <summary>
    /// Представляє контекст бази даних, який забезпечує зв'язок між класом моделі підказки <see cref="Tip"/> і фізичною базою даних.
    /// </summary>
    public class TipsApiDbContext : DbContext
    {
        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="TipsApiDbContext"/>, використовуючи базовий конструктор класу <see cref="DbContext"/> та зазначені налаштування.
        /// </summary>
        /// <param name="options">Налаштування контексту.</param>
        public TipsApiDbContext(DbContextOptions<TipsApiDbContext> options) : base(options)
        {
        }

        #endregion

        #region Властивості

        /// <summary>
        /// Повертає або встановлює набір всіх сутностей у базі даних у контексті класу <see cref="Tip"/>.
        /// </summary>
        /// <returns>
        /// Набір всіх сутностей у базі даних у контексті класу <see cref="Tip"/> як <see cref="DbSet{TEntity}"/> з елементів класу <see cref="Tip"/>.
        /// </returns>
        public DbSet<Tip> Tips { get; set; }

        #endregion
    }
}
