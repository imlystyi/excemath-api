using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// Представляє модель розв'язаної математичної проблеми, яка має унікальний ідентифікатор, тип, умову, правильний розв'язок та покроковий розв'язок.
/// </summary>
public class SolvedMathProblem
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

    /// <summary>
    /// Повертає або встановлює покроковий розв'язок математичної проблеми.
    /// </summary>
    /// <returns>
    /// Покроковий розв'язок математичної проблеми як <see cref="string"/>.
    /// </returns>
    public string Solution { get; set; }

    #endregion
}
