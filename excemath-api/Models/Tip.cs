// Зробити окрему базу даних для загальних підказок, чи зберігати їх як поля математичних проблем?
// Подумати над цим; моделі, контролери, контексти для Tip поки що не робити!

namespace excemathApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Tip
    {
        #region Властивості

        /// <summary>
        /// 
        /// </summary>
        public MathProblemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        #endregion
    }
}
