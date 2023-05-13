namespace excemathApi.Models;

/// <summary>
/// Представляє рейтинг користувача, який має унікальний псевдонім і рейтинг.
/// </summary>
public class UserRating
{
    #region Властивості

    /// <inheritdoc cref="User.Nickname"/>s
    public string Nickname { get; set; }

    /// <summary>
    /// Повертає або встановлює рейтинг поточного користувача.
    /// </summary>
    public double Rating { get; set; }

    #endregion
}
