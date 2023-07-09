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
}
