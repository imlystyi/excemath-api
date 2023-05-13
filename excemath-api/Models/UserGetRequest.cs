namespace excemathApi.Models;

/// <summary>
/// Представляє користувача для запиту отримання, який має унікальний псевдонім, кількість правильних та неправильних відповідей.
/// </summary>
public class UserGetRequest
{
    #region Властивості

    /// <inheritdoc cref="User.Nickname"/>
    public string Nickname { get; set; }

    /// <inheritdoc cref="User.RightAnswers"/>
    public int RightAnswers { get; set; }

    /// <inheritdoc cref="User.WrongAnswers"/>
    public int WrongAnswers { get; set; }

    #endregion
}
