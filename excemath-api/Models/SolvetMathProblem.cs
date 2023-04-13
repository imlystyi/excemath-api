#region Usings-частина

using System.ComponentModel.DataAnnotations.Schema;

#endregion
namespace excemathApi.Models
{
    public class SolvetMathProblem
    {
        #region Властивості

        /// <summary>
        /// Повертає або встановлює унікальний ідентифікатор математичної проблеми.
        /// </summary>
        /// <returns>
        /// Унікальний ідентифікатор математичної проблеми як <see cref="int"/>. Є первинним ключом.
        /// </returns>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Повертає або встановлює питання математичної проблеми.
        /// </summary>
        /// <returns>
        /// Питання математичної проблеми як <see cref="string"/>.
        /// </returns>
        public string Question { get; set; }

        /// <summary>
        /// Повертає або встановлює правильний розв'язок математичної проблеми.
        /// </summary>
        /// <returns>
        /// Правильний розв'язок математичної проблеми як <see cref="string"/>.
        /// </returns>
        public string Answer { get; set; }

        /// <summary>
        /// Повертає або встановлює пкороковий розв'язок математичної проблеми.
        /// </summary>
        /// <returns>
        public string Solution { get; set; }

        #endregion
    }
}
