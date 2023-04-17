namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель користувача запиту отримання, описану звичайною моделлю <see cref="User"/>, яка має унікальний псевдонім, кількість правильних та неправильних відповідей.
    /// </summary>
    public class GetUserRequest
    {
        #region Властивості

        /// <inheritdoc cref="User.Nickname"/>
        public string Nickname { get; set; }

        /// <inheritdoc cref="User.RightAnswers"/>
        public int RightAnswers { get; set; }

        /// <inheritdoc cref="User.WrongAnswers"/>
        public int WrongAnswers { get; set; }

        #endregion

        #region Перевизначені оператори

        // Дозволяє звести тип звичайної моделі користувача до моделі користувача запиту отримання.
        public static explicit operator GetUserRequest(User user) => new()
        {
            Nickname = user.Nickname,
            RightAnswers = user.RightAnswers,
            WrongAnswers = user.WrongAnswers
        };

        #endregion
    }
}
