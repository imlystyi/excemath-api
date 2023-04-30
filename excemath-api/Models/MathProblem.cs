namespace excemathApi.Models;

/// <summary>
/// Представляє модель математичної проблеми, яка має унікальний ідентифікатор, тип, умову, правильний розв'язок та загальну підказку.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Id"/>.
/// </remarks>
public class MathProblem
{
    #region Властивості

    /// <summary>
    /// Повертає унікальний ідентифікатор математичної проблеми.
    /// </summary>
    /// <returns>
    /// Унікальний ідентифікатор математичної проблеми як <see cref="int"/>. Є первинним ключом.
    /// </returns>
    public int Id { get; set; }

    /// <summary>
    /// Повертає вид математичної проблеми, представлений елементом перерахування <see cref="MathProblemKinds"/>.
    /// </summary>
    /// <returns>
    /// Вид математичної проблеми як елемент перерахування <see cref="MathProblemKinds"/>.
    /// </returns>
    public MathProblemKinds Kind { get; set; }

    /// <summary>
    /// Повертає питання математичної проблеми.
    /// </summary>
    /// <returns>
    /// Питання математичної проблеми як <see cref="string"/>.
    /// </returns>
    public string Question { get; set; }

    /// <summary>
    /// Повертає правильний розв'язок математичної проблеми.
    /// </summary>
    /// <returns>
    /// Правильний розв'язок математичної проблеми як <see cref="string"/>.
    /// </returns>
    public string Answer { get; set; }

    // TODO: видалити #5
    /// <summary>
    /// Повертає загальну підказку математичної проблеми.
    /// </summary>
    /// <returns>
    /// Загальну підказку математичної проблеми як<see cref="string"/>.
    /// </returns>
    //public string Tip =>
    //        // TODO: розписати підказки (використовуючи LaTeX).
    //        Kind switch
    //        {
    //            MathProblemKinds.TableIntegral => "",
    //            MathProblemKinds.LineIntegral => "підказка для лінійного інтеграла",

    //            MathProblemKinds.Matrix => "підказка для матриць",
    //            MathProblemKinds.Limit => "підказка для границь",

    //            MathProblemKinds.LinearEquation => "підказка для лінійних рівнянь",
    //            MathProblemKinds.QuadraticEquation => "підказка для квадратних рівнянь",
    //            MathProblemKinds.IrrationalEquation => "підказка для ірраціональних рівнянь",
    //            MathProblemKinds.ExponentialEquation => "підказка для показникових рівнянь",
    //            MathProblemKinds.LogarithmicEquation => "підказка для логарифмічних рівнянь",
    //            MathProblemKinds.TrigonometricEquation => "підказка для тригонометричних рівнянь",

    //            MathProblemKinds.LinearInequality => "підказка для лінійних нерівностей ",
    //            MathProblemKinds.QuadraticInequality => "підказка для квадратичних нерівностей ",
    //            MathProblemKinds.IrrationalInequality => "підказка для ірраціональних нерівностей ",
    //            MathProblemKinds.ExponentialInequality => "підказка для показникових нерівностей ",
    //            MathProblemKinds.LogarithmicInequality => "підказка для логарифмічних нерівностей ",
    //            MathProblemKinds.TrigonometricInequality => "підказка для тригонометричних нерівностей ",

    //            MathProblemKinds.NumericalSequences => "підказка для числових послідовностях",
    //            MathProblemKinds.Function => "підказка для функцій",
    //            MathProblemKinds.Combinatorics => "підказка для комбінаторики",

    //            _ => throw new ArgumentException("Некоректний вид математичної проблеми", nameof(Kind))
    //        };

    #endregion
}
