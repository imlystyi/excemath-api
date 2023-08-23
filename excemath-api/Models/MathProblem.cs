﻿namespace excemathApi.Models;

/// <summary>
/// Represents a math problem with the unique identifier, type, difficulty, question, answer options list and step-by-step solution.
/// </summary>
public class MathProblem
{
    #region Properties

    /// <summary>
    /// The math problem identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A math problem type.
    /// </summary>
    public MathProblemTypes Type { get; set; }

    /// <summary>
    /// The math problem difficulty.
    /// </summary>
    public int Difficulty { get; set; }

    /// <summary>
    /// The math problem question.
    /// </summary>
    public Exposition Question { get; set; }

    /// <summary>
    /// The answer options list.
    /// </summary>
    public List<Option> Options { get; set; }

    /// <summary>
    /// The index of the correct answer in the answer options list.
    /// </summary>
    public int Answer { get; set; }

    #nullable enable

    /// <summary>
    /// The step-by-step solution of the math problem.
    /// </summary>
    public List<Exposition>? Solution { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MathProblem"/> class.
    /// </summary>
    public MathProblem() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathProblem"/> class using an exist <see cref="MathProblemDto"/> class instance.
    /// </summary>
    /// <param name="dto">The Data Transfer Object from which the member values will be taken.</param>
    public MathProblem(MathProblemDto dto)
    {
        this.Id = dto.Id;
        this.Type = dto.Type;
        this.Difficulty = dto.Difficulty;
        this.Question = new(dto.QuestionNormalText, dto.QuestionLatex);
        this.Options = GetOptions(dto.OptionsRenderAsLatexOrder, dto.OptionsNumberOrder,
            dto.OptionsValueOrder);
        this.Answer = dto.Answer;
        this.Solution = GetSolution(dto.SolutionNormalTextsOrder, dto.SolutionLatexOrder);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Converts a current math problem model to its Data Transfer Object model.
    /// </summary>
    public MathProblemDto ToDto() => new()
    {
        Id = this.Id,
        Type = this.Type,
        Difficulty = this.Difficulty,
        QuestionNormalText = this.Question.NormalText,
        QuestionLatex = this.Question.Latex,
        OptionsRenderAsLatexOrder = this.Options.ConvertAll(oo => oo.RenderAsLatex),
        OptionsNumberOrder = this.Options.ConvertAll(oo => oo.Number),
        OptionsValueOrder = this.Options.ConvertAll(oo => oo.Value),
        Answer = this.Answer,
        SolutionNormalTextsOrder = this.Solution?.Select(ss => ss.NormalText).ToList(),
        SolutionLatexOrder = this.Solution?.Select(ss => ss.NormalText).ToList()
    };

    private static List<Option> GetOptions(IReadOnlyList<bool> renderAsLatexOrder, IReadOnlyList<int> numberOrder, IReadOnlyList<string> valueOrder)
    {
        int count;

        if ((count = renderAsLatexOrder.Count) != numberOrder.Count || count != valueOrder.Count)
            throw new ArgumentException("The orders must have the same length.");

        List<Option> options = new();

        for (int ii = 0; ii < count; ii++)
            options.Add(new(renderAsLatexOrder[ii], numberOrder[ii], valueOrder[ii]));

        return options;
    }

    private static List<Exposition>? GetSolution(List<string?>? normalTextsOrder, List<string?>? latexOrder)
    {
        if (normalTextsOrder is null || latexOrder is null)
            return null;

        int length;

        if ((length = normalTextsOrder.Count) != latexOrder.Count)
            throw new ArgumentException("The orders must have the same length.");

        List<Exposition> solution = new();

        for (int ii = 0; ii < length; ii++)
            solution.Add(new(normalTextsOrder[ii], latexOrder[ii]));

        return solution;
    }

    #endregion
}
