namespace excemathApi.Models;

/// <summary>
/// Представляє перерахування можливих видів для моделі математичної проблеми <see cref="MathProblem"/> або <see cref="SolvedMathProblem"/> (<see cref="MathProblem.Kind"/> або <see cref="SolvedMathProblem.Kind"/>).
/// </summary>
public enum MathProblemKinds
{
    /// <summary>
    /// Представляє вид "Табличні інтеграли".
    /// </summary>
    TableIntegral,

    /// <summary>
    /// Представляє вид "Кратні інтеграли".
    /// </summary>
    MultipleIntegral,

    /// <summary>
    /// Представляє вид "Криволінійні інтеграли".
    /// </summary>
    LineIntegral,

    /// <summary>
    /// Представляє вид "Матриці".
    /// </summary>
    Matrix,

    /// <summary>
    /// Представляє вид "Границі".
    /// </summary>
    Limit,

    /// <summary>
    /// Представляє вид "Лінійні рівняння".
    /// </summary>
    LinearEquation,

    /// <summary>
    /// Представляє вид "Квадратні рівняння".
    /// </summary>
    QuadraticEquation,

    /// <summary>
    /// Представляє вид "Ірраціональні рівняння".
    /// </summary>
    IrrationalEquation,

    /// <summary>
    /// Представляє вид "Показникові рівняння".
    /// </summary>
    ExponentialEquation,

    /// <summary>
    /// Представляє вид "Логарифмічні рівняння".
    /// </summary>
    LogarithmicEquation,

    /// <summary>
    /// Представляє вид "Тригонометричні рівняння".
    /// </summary>
    TrigonometricEquation,

    /// <summary>
    /// Представляє вид "Лінійні нерівності".
    /// </summary>
    LinearInequality,

    /// <summary>
    /// Представляє вид "Квадратичні нерівності".
    /// </summary>
    QuadraticInequality,

    /// <summary>
    /// Представляє вид "Ірраціональні нерівності".
    /// </summary>
    IrrationalInequality,

    /// <summary>
    /// Представляє вид "Показникові нерівності".
    /// </summary>
    ExponentialInequality,

    /// <summary>
    /// Представляє вид "Логарифмічні нерівності".
    /// </summary>
    LogarithmicInequality,

    /// <summary>
    /// Представляє вид "Тригонометричні нерівності".
    /// </summary>
    TrigonometricInequality,

    /// <summary>
    /// Представляє вид "Числові послідовності".
    /// </summary>
    NumericalSequences,

    /// <summary>
    /// Представляє вид "Функції".
    /// </summary>
    Function,

    /// <summary>
    /// Представляє вид "Комбінаторика".
    /// </summary>
    Combinatorics
}
