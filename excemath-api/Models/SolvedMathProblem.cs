using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// Представляє модель розв'язаної математичної проблеми, яка має унікальний ідентифікатор, вид, питання, правильну відповідь та покроковий розв'язок.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Id"/>.
/// </remarks>
public class SolvedMathProblem
{
    #region Властивості

    /// <summary>
    /// Повертає або встановлює унікальний ідентифікатор розв'язаної математичної проблеми.
    /// </summary>
    /// <returns>
    /// Унікальний ідентифікатор розв'язаної математичної проблеми як <see cref="int"/>. Є первинним ключом.
    /// </returns>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    /// <summary>
    /// Повертає або встановлює вид розв'язаної математичної проблеми, представлений елементом перерахування <see cref="MathProblemKinds"/>.
    /// </summary>
    /// <returns>
    /// Вид розв'язаної математичної проблеми як елемент перерахування <see cref="MathProblemKinds"/>.
    /// </returns>
    public MathProblemKinds Kind { get; set; }

    /// <summary>
    /// Повертає або встановлює питання розв'язаної математичної проблеми.
    /// </summary>
    /// <returns>
    /// Питання розв'язаної математичної проблеми як <see cref="string"/>.
    /// </returns>
    public string Question { get; set; }

    /// <summary>
    /// Повертає або встановлює правильну відповідь розв'язаної математичної проблеми.
    /// </summary>
    /// <returns>
    /// Правильну відповідь розв'язаної математичної проблеми як <see cref="string"/>.
    /// </returns>
    public string Answer { get; set; }

    /// <summary>
    /// Повертає або встановлює покроковий розв'язок розв'язаної математичної проблеми.
    /// </summary>
    /// <returns>
    /// Покроковий розв'язок розв'язаної математичної проблеми як <see cref="string"/>.
    /// </returns>
    public string Solution { get; set; }

    #endregion
}
