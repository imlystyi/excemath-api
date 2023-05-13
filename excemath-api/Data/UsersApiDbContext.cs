using excemathApi.Models;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Data;

/// <summary>
/// Представляє контекст бази даних, який забезпечує зв'язок між об'єктами класу <see cref="User"/> і фізичною базою даних.
/// </summary>
public class UsersApiDbContext : DbContext
{
    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UsersApiDbContext"/>, використовуючи базовий конструктор класу <see cref="DbContext"/> та зазначені налаштування.
    /// </summary>
    /// <param name="options">Налаштування контексту.</param>
    public UsersApiDbContext(DbContextOptions<UsersApiDbContext> options) : base(options)
    {
    }

    #endregion

    #region Властивості

    /// <summary>
    /// Повертає або встановлює набір всіх сутностей у базі даних у контексті класу <see cref="User"/>.
    /// </summary>
    public DbSet<User> Users { get; set; }

    #endregion
}
