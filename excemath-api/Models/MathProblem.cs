﻿#region Usings-частина

using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель математичної проблеми, яка має унікальний ідентифікатор, тип, умову і розв'язок.
    /// </summary>
    /// <remarks>
    /// Має первинний ключ <see cref="Id"/>.
    /// </remarks>
    public class MathProblem
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
        /// Повертає або встановлює вид математичної проблеми, представлений елементом перерахування <see cref="MathProblemKinds"/>.
        /// </summary>
        /// <returns>
        /// Вид математичної проблеми як елемент перерахування <see cref="MathProblemKinds"/>.
        /// </returns>
        public MathProblemKinds Kind { get; set; }

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

        #endregion
    }
}
