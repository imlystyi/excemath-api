namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель користувача запиту оновлення, описану звичайною моделлю <see cref="User"/>, яка має пароль, кількість правильних та неправильних відповідей.
    /// </summary>
    public class UpdateUserRequest
    {
        /// <inheritdoc cref="User.Password"/>
        public string Password { get; set; }

        /// <inheritdoc cref="User.RightAnswers"/>
        public int RightAnswers { get; set; }

        /// <inheritdoc cref="User.WrongAnswers"/>
        public int WrongAnswers { get; set; }
    }
}
