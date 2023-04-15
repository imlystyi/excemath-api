namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель користувача запиту додавання (описану звичайною моделлю <see cref="User"/>) яка має унікальний псевдонім та пароль.
    /// </summary>
    public class AddUserRequest
    {
        /// <inheritdoc cref="User.Nickname"/>
        public string Nickname { get; set; }

        /// <inheritdoc cref="User.Password"/>
        public string Password { get; set; }
    }
}
