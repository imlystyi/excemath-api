using Microsoft.EntityFrameworkCore;

namespace excemathApi.Models;

/// <summary>
/// Представляє звичайну модель користувача, яка має унікальний псевдонім, пароль, кількість правильних та неправильних відповідей.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Nickname"/>.
/// </remarks>
[PrimaryKey(nameof(Nickname))]
public class User
{
    #region Властивості 

    /// <summary>
    /// Повертає або встановлює псевдонім користувача.
    /// </summary>
    /// <returns>
    /// Псевдонім користувача як <see cref="string"/>. Є первинним ключом.
    /// </returns>
    public string Nickname { get; set; }

    /// <summary>
    /// Повертає або встановлює пароль користувача.
    /// </summary>
    /// <returns>
    /// Пароль користувача як <see cref="string"/>.
    /// </returns>
    public string Password { get; set; }

    /// <summary>
    /// Повертає або встановлює кількість правильних відповідей користувача.
    /// </summary>
    /// <remarks>
    /// Має значення 0 за замовчуванням.
    /// </remarks>
    /// <returns>
    /// Кількість правильних відповідей користувача як <see cref="int"/>.
    /// </returns>
    public int RightAnswers { get; set; } = 0;

    /// <summary>
    /// Повертає або встановлює кількість неправильних відповідей користувача.
    /// </summary>
    /// <remarks>
    /// Має значення 0 за замовчуванням.
    /// </remarks>
    /// <returns>
    /// Кількість неправильних відповідей користувача як <see cref="int"/>.
    /// </returns>
    public int WrongAnswers { get; set; } = 0;

    #endregion
}
