namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель, описану класом <see cref="User"/>, яка використовується для запиту оновлення об'єкта цього класу в базі даних.
    /// </summary>
    public class UpdateUserRequest
    {
        /// <inheritdoc cref="User.Nickname"/>
        public string Nickname { get; set; }

        /// <inheritdoc cref="User.Password"/>
        public string Password { get; set; }

        /// <inheritdoc cref="User.RightAnswers"/>
        public int RightAnswers { get; set; }

        /// <inheritdoc cref="User.WrongAnswers"/>
        public int WrongAnswers { get; set; }
    }
}
