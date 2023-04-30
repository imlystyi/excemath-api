namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель рейтингу користувача (відповідну до моделі <see cref="User"/>), яка має унікальний псевдонім і рейтинг.
    /// </summary>
    public class UserRating
    {
        #region Властивості

        /// <inheritdoc cref="User.Nickname"/>s
        public string Nickname { get; set; }

        /// <summary>
        /// Повертає або встановлює рейтинг користувача.
        /// </summary>
        /// <returns>
        /// Рейтинг користувача як <see cref="double"/>.
        /// </returns>
        public double Rating { get; set; }

        #endregion
    }
}
