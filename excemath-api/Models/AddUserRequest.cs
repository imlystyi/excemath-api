namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель, описану класом <see cref="User"/>, яка використовується для запиту додавання об'єкта цього класу в базу  даних.
    /// </summary>
    public class AddUserRequest
    {
        /// <inheritdoc cref="User.Nickname"/>
        public string Nickname { get; set; }

        /// <inheritdoc cref="User.Password"/>
        public string Password { get; set; }
    }
}
