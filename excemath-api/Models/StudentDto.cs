// excemath API - open source API for educational projects related to mathematics
// Copyright (C) 2023  miu-miu enjoyers
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Contact us:
// i.   By paper mail: 23 Yevhena Patona street, Zaliznychnyi raion, Lviv, Lviv oblast, 79040, Ukraine
// ii.  By email: vladyslav.yakubovskyi.work@gmail.com
//
// See the official repository page on GitHub: <https://github.com/miu-miu-enjoyers/excemath-api>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// Represents a <see cref="Student"/> class object as a Data Transfer Object.
/// </summary>
[Table("Students")]
public class StudentDto
{
    /// <inheritdoc cref="Student.Id"/>
    /// <remarks>Required.</remarks>
    [Key]
    public required Guid Id { get; set; }

    /// <inheritdoc cref="Student.Nickname"/>
    /// <remarks>Required.</remarks>
    public required string Nickname { get; set; }

    /// <inheritdoc cref="Student.FirstName"/>
    public string FirstName { get; set; } = string.Empty;

    /// <inheritdoc cref="Student.LastName"/>
    public string LastName { get; set; } = string.Empty;

    /// <inheritdoc cref="Student.SolvedMathProblems"/>
    public List<Guid> SolvedMathProblems { get; set; } = new();

    /// <inheritdoc cref="Student.Experience"/>
    public int Experience { get; set; }

    /// <summary>
    /// Gets or sets the order of the correct answers in the <see cref="Student.CorrectAnswers"/> property.
    /// </summary>
    public List<int> CorrectAnswersOrder { get; set; } = Enumerable.Repeat(0, Enum.GetValues(typeof(MathProblemTypes)).Length).ToList();

    /// <summary>
    /// Gets or sets the order of the incorrect answers in the <see cref="Student.IncorrectAnswers"/> property.
    /// </summary>
    public List<int> IncorrectAnswersOrder { get; set; } = Enumerable.Repeat(0, Enum.GetValues(typeof(MathProblemTypes)).Length).ToList();

    /// <inheritdoc cref="Student.Location"/>
    public string Location { get; set; } = string.Empty;

    /// <inheritdoc cref="Student.About"/>
    public string About { get; set; } = string.Empty;
}
