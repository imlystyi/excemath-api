/* excemath - an app for preparing for math exams.
* Copyright (C) 2023 miu-miu enjoyers

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program. If not, see <https://www.gnu.org/licenses/>. */

namespace excemathApi.Models;

/// <summary>
/// Представляє перерахування можливих видів завдання для об'єктів класів <see cref="MathProblem"/> та <see cref="SolvedMathProblem"/>.
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
    Function
}
