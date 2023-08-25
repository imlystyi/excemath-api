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
