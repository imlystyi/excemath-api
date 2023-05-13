namespace excemathApi.Models;

/// <summary>
/// Представляє користувача для запиту оновлення, який має пароль, кількість правильних та неправильних відповідей.
/// </summary>
public class UserUpdateRequest
{
    #region Властивості

    /// <inheritdoc cref="User.Password"/>
    public string Password { get; set; }

    /// <inheritdoc cref="User.RightAnswers"/>
    public int RightAnswers { get; set; }

    /// <inheritdoc cref="User.WrongAnswers"/>
    public int WrongAnswers { get; set; }

    #endregion
}
