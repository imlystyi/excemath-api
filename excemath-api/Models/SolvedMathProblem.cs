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
/// Представляє розв'язану математичну задачу, яка має унікальний ідентифікатор, вид, питання, правильну відповідь та покроковий розв'язок.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Id"/>.
/// </remarks>
public class SolvedMathProblem
{
    /// <inheritdoc cref="MathProblem.Id"/>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    /// <inheritdoc cref="MathProblem.Kind"/>
    public MathProblemKinds Kind { get; set; }

    /// <inheritdoc cref="MathProblem.Question"/>
    public string Question { get; set; }

    /// <inheritdoc cref="MathProblem.Answer"/>
    public string Answer { get; set; }

    /// <summary>
    /// Повертає або встановлює покроковий розв'язок у поточній задачі.
    /// </summary>
    public string Solution { get; set; }
}
