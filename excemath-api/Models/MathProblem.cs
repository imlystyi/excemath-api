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

namespace excemathApi.Models;

/// <summary>
/// Represents a math problem with the unique identifier, type, difficulty, question, answer options list and step-by-step solution.
/// </summary>
public class MathProblem
{
    #region Properties

    /// <summary>
    /// Gets or sets the unique math problem identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a math problem type.
    /// </summary>
    public MathProblemTypes Type { get; set; }

    /// <summary>
    /// Gets or sets the math problem difficulty.
    /// </summary>
    public int Difficulty { get; set; }

    /// <summary>
    /// Gets or sets the math problem question.
    /// </summary>
    public MathExposition Question { get; set; }

    /// <summary>
    /// Gets or sets the answer options list.
    /// </summary>
    public List<MathOption> Options { get; set; }

    /// <summary>
    /// Gets or sets the index of the correct answer in the answer options list.
    /// </summary>
    public int AnswerIndex { get; set; }

    #nullable enable

    /// <summary>
    /// Gets or sets the step-by-step solution of the math problem.
    /// </summary>
    public List<MathExposition>? Solution { get; set; }

    /// <summary>
    /// Gets or sets the attributes list of the math problem.
    /// </summary>
    public List<string>? Attributes { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MathProblem"/> class.
    /// </summary>
    public MathProblem() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathProblem"/> class using an exist <see cref="MathProblemDto"/> class instance.
    /// </summary>
    /// <param name="dto">The Data Transfer Object from which the properties value will be taken.</param>
    public MathProblem(MathProblemDto dto)
    {
        this.Id = dto.Id;
        this.Type = dto.Type;
        this.Difficulty = dto.Difficulty;
        this.Question = new(dto.QuestionNormalText, dto.QuestionLatex);
        this.Options = GetOptions(dto.OptionsRenderAsLatexOrder, dto.OptionsIndexOrder, dto.OptionsContentOrder);
        this.AnswerIndex = dto.AnswerIndex;
        this.Solution = GetSolution(dto.SolutionNormalTextsOrder, dto.SolutionLatexOrder);
        this.Attributes = dto.Attributes;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Creates a new instance of the <see cref="MathProblemDto"/> class from the current object.
    /// </summary>
    public MathProblemDto ToDto() => new()
    {
        Id = this.Id,
        Type = this.Type,
        Difficulty = this.Difficulty,
        QuestionNormalText = this.Question.NormalText,
        QuestionLatex = this.Question.Latex,
        OptionsRenderAsLatexOrder = this.Options.ConvertAll(oo => oo.RenderAsLatex),
        OptionsIndexOrder = this.Options.ConvertAll(oo => oo.Index),
        OptionsContentOrder = this.Options.ConvertAll(oo => oo.Content),
        AnswerIndex = this.AnswerIndex,
        SolutionNormalTextsOrder = this.Solution?.ConvertAll(ss => ss.NormalText),
        SolutionLatexOrder = this.Solution?.ConvertAll(ss => ss.Latex)
    };

    private static List<MathOption> GetOptions(IReadOnlyList<bool> renderAsLatexOrder, IReadOnlyList<int> numberOrder,
        IReadOnlyList<string> valueOrder)
    {
        int count;

        if ((count = renderAsLatexOrder.Count) != numberOrder.Count || count != valueOrder.Count)
            throw new ArgumentException("The orders must have the same length.");

        List<MathOption> options = new();

        for (int ii = 0; ii < count; ii++)
            options.Add(new(renderAsLatexOrder[ii], numberOrder[ii], valueOrder[ii]));

        return options;
    }

    private static List<MathExposition>? GetSolution(List<string?>? normalTextsOrder, List<string?>? latexOrder)
    {
        if (normalTextsOrder is null || latexOrder is null)
            return null;

        int length;

        if ((length = normalTextsOrder.Count) != latexOrder.Count)
            throw new ArgumentException("The orders must have the same length.");

        List<MathExposition> solution = new();

        for (int ii = 0; ii < length; ii++)
            solution.Add(new(normalTextsOrder[ii], latexOrder[ii]));

        return solution;
    }

    #endregion
}
