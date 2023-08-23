using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// Represents a <see cref="MathProblem"/> as a Data Transfer Object.
/// </summary>
[Table("MathProblems")]
public class MathProblemDto
{
    /// <inheritdoc cref="MathProblem.Id"/>
    [Key]
    public required Guid Id { get; set; }

    /// <inheritdoc cref="MathProblem.Type"/>
    public required MathProblemTypes Type { get; set; }

    /// <inheritdoc cref="MathProblem.Difficulty"/>
    public required int Difficulty { get; set; }

    /// <summary>
    /// A normal text in the math problem question.
    /// </summary>
    /// <remarks> Required. </remarks>
    public required string QuestionNormalText { get; set; }

#nullable enable 

    /// <summary>
    /// A LaTeX part in the math problem question.
    /// </summary>
    public string? QuestionLatex { get; set; }

#nullable restore

    /// <summary>
    /// The order of the <see cref="Option.RenderAsLatex"/> property values of the options.
    /// </summary>
    /// <remarks> Required. </remarks>
    public required List<bool> OptionsRenderAsLatexOrder { get; set; }

    /// <summary>
    /// The order of the <see cref="Option.Number"/> property values of the options.
    /// </summary>
    /// <remarks> Required. </remarks>
    public required List<int> OptionsNumberOrder { get; set; }

    /// <summary>
    /// The order of the <see cref="Option.Value"/> property values of the options.
    /// </summary>
    /// <remarks> Required. </remarks>
    public required List<string> OptionsValueOrder { get; set; }

    /// <inheritdoc cref="MathProblem.Answer"/>
    public required int Answer { get; set; }

#nullable enable

    /// <summary>
    /// The order of the <see cref="Exposition.NormalText"/> property values of the step-by-step solution.
    /// </summary>
    public List<string?>? SolutionNormalTextsOrder { get; set; }
    
    /// <summary>
    /// The order of the <see cref="Exposition.Latex"/> property values of the step-by-step solution.
    /// </summary>
    public List<string?>? SolutionLatexOrder { get; set; }
}
