#nullable disable // Вимикаємо доступність nullable-значень: у цьому класі вони не використовуються.

namespace excemathApi.Models
{
    public class AddMathProblemRequest
    {
        #region Властивості
        /// <summary>
        /// Повертає або встановлює вид математичної проблеми, представлений елементом перерахування <see cref="ProblemKinds"/>.
        /// </summary>
        /// <returns>
        /// Вид математичної проблеми у вигляді елемента перерахування <see cref="ProblemKinds"/>.
        /// </returns>
        public ProblemKinds Kind { get; set; }

        /// <summary>
        /// Повертає або встановлює питання математичної проблеми.
        /// </summary>
        /// <returns>
        /// Питання математичної проблеми.
        /// </returns>
        public string Question { get; set; }

        /// <summary>
        /// Повертає або встановлює правильний розв'язок математичної проблеми.
        /// </summary>
        /// <returns>
        /// Правильний розв'язок математичної проблеми.
        /// </returns>
        public string Answer { get; set; }
        #endregion
    }
}
