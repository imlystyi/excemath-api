using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// Представляє математичну задачу, яка має унікальний ідентифікатор, вид, питання та правильну відповідь.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Id"/>.
/// </remarks>
public class MathProblem
{
    #region Властивості

    /// <summary>
    /// Повертає або встановлює унікальний ідентифікатор поточної задачі.
    /// </summary>
    /// <remarks>
    /// Є первинним ключом.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    /// <summary>
    /// Повертає або встановлює вид поточної задачі, який представлений елементом перерахування <see cref="MathProblemKinds"/>.
    /// </summary>
    public MathProblemKinds Kind { get; set; }

    /// <summary>
    /// Повертає або встановлює питання поточної задачі.
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    /// Повертає або встановлює правильну відповідь поточної задачі.
    /// </summary>
    public string Answer { get; set; }

    #endregion
}
