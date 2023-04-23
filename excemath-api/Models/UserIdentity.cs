namespace excemathApi.Models;

/// <summary>
/// Представляє модель ідентичності користувача (відповідну до моделі <see cref="User"/>), яка має унікальний псевдонім та пароль.
/// </summary>
public class UserIdentity
{
    #region Властивості

    /// <inheritdoc cref="User.Nickname"/>
    public string Nickname { get; set; }

    /// <inheritdoc cref="User.Password"/>
    public string Password { get; set; }

    #endregion
}
