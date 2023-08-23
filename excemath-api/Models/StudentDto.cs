using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// 
/// </summary>
[Table("Students")]
public class StudentDto
{
    /// <summary>
    /// 
    /// </summary>
    public required Guid Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public required string Nickname { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public List<Guid> SolvedMathProblems { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    public int Experience { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public List<int> CorrectAnswersOrder { get; set; } = Enumerable.Repeat(0, Enum.GetValues(typeof(MathProblemTypes)).Length).ToList();

    /// <summary>
    /// 
    /// </summary>
    public List<int> IncorrectAnswersOrder { get; set; } = Enumerable.Repeat(0, Enum.GetValues(typeof(MathProblemTypes)).Length).ToList();

    /// <summary>
    /// 
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string About { get; set; } = string.Empty;
}
