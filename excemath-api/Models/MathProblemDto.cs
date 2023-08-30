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
/// Represents a <see cref="MathProblem"/> class object as a Data Transfer Object.
/// </summary>
[Table("MathProblems")]
public class MathProblemDto
{
    /// <inheritdoc cref="MathProblem.Id"/>
    /// <remarks>Required.</remarks>
    [Key]
    public required Guid Id { get; set; }

    /// <inheritdoc cref="MathProblem.Type"/>
    /// <remarks>Required.</remarks>
    public required MathProblemTypes Type { get; set; }

    /// <inheritdoc cref="MathProblem.Difficulty"/>
    /// <remarks>Required.</remarks>
    public required int Difficulty { get; set; }

    /// <summary>
    /// Gets or sets a normal text part in the math problem question.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required string QuestionNormalText { get; set; }

    #nullable enable

    /// <summary>
    /// Gets or sets a LaTeX part in the math problem question.
    /// </summary>
    public string? QuestionLatex { get; set; }

    #nullable restore

    /// <summary>
    /// Gets or sets the order of the <see cref="MathOption.RenderAsLatex"/> property values of the options.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required List<bool> OptionsRenderAsLatexOrder { get; set; }

    /// <summary>
    /// Gets or sets the order of the <see cref="MathOption.Index"/> property values of the options.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required List<int> OptionsIndexOrder { get; set; }

    /// <summary>
    /// Gets or sets the order of the <see cref="MathOption.Content"/> property values of the options.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required List<string> OptionsContentOrder { get; set; }

    /// <inheritdoc cref="MathProblem.AnswerIndex"/>
    public required int AnswerIndex { get; set; }

    #nullable enable

    /// <summary>
    /// Gets or sets the order of the <see cref="MathExposition.NormalText"/> property values of the step-by-step solution.
    /// </summary>
    public List<string?>? SolutionNormalTextsOrder { get; set; }

    /// <summary>
    /// Gets or sets the order of the <see cref="MathExposition.Latex"/> property values of the step-by-step solution.
    /// </summary>
    public List<string?>? SolutionLatexOrder { get; set; }

    /// <inheritdoc cref="MathProblem.Attributes"/>
    public List<string>? Attributes { get; set; }
}
