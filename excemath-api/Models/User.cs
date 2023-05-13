using Microsoft.EntityFrameworkCore;

namespace excemathApi.Models;

/// <summary>
/// Представляє користувача, який має унікальний псевдонім, пароль, кількість правильних та неправильних відповідей.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Nickname"/>.
/// </remarks>
[PrimaryKey(nameof(Nickname))]
public class User
{
    #region Властивості 

    /// <summary>
    /// Повертає або встановлює унікальний псевдонім поточного користувача.
    /// </summary>
    /// <remarks>
    /// Є первинним ключом.
    /// </remarks>
    public string Nickname { get; set; }

    /// <summary>
    /// Повертає або встановлює пароль поточного користувача.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Повертає або встановлює кількість правильних відповідей поточного користувача.
    /// </summary>
    /// <remarks>
    /// Має значення 0 за замовчуванням.
    /// </remarks>
    public int RightAnswers { get; set; } = 0;

    /// <summary>
    /// Повертає або встановлює кількість неправильних відповідей поточного користувача.
    /// </summary>
    /// <remarks>
    /// Має значення 0 за замовчуванням.
    /// </remarks>
    public int WrongAnswers { get; set; } = 0;

    #endregion
}
