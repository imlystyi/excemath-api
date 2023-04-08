// Передивитися, чи треба користувачу мати властивість Id, якщо ми можемо позначити його логін як PK, який не буде повторюватись.

using Microsoft.EntityFrameworkCore;

namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель користувача, який має унікальний ідентифікатор, псевдонім, пароль, кількість правильних та неправильних відповідей.
    /// </summary>
    /// <remarks>
    /// Має первинний ключ <see cref="Nickname"/>.
    /// </remarks>
    [PrimaryKey(nameof(Nickname))]
    public class User
    {
        #region Властивості 

        ///// <summary>
        ///// Повертає або встановлює унікальний ідентифікатор користувача.
        ///// </summary>
        ///// <returns>
        ///// Унікальний ідентифікатор користувача як <see cref="Guid"/>.
        ///// </returns>
        //public Guid Id { get; set; }

        /// <summary>
        /// Повертає або встановлює псевдонім користувача.
        /// </summary>
        /// <returns>
        /// Псевдонім користувача як <see cref="string"/>. Є первинним ключом.
        /// </returns>
        public string Nickname { get; set; }

        /// <summary>
        /// Повертає або встановлює пароль користувача.
        /// </summary>
        /// <returns>
        /// Пароль користувача як <see cref="string"/>.
        /// </returns>
        public string Password { get; set; }

        /// <summary>
        /// Повертає або встановлює кількість правильних відповідей користувача.
        /// </summary>
        /// <returns>
        /// Кількість правильних відповідей користувача як <see cref="int"/>.
        /// </returns>
        public int RightAnswers { get; set; } = 0;

        /// <summary>
        /// Повертає або встановлює кількість неправильних відповідей користувача.
        /// </summary>
        /// <returns>
        /// Кількість неправильних відповідей користувача як <see cref="int"/>.
        /// </returns>
        public int WrongAnswers { get; set; } = 0;

        #endregion
    }
}
