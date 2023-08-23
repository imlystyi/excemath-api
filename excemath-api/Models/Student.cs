namespace excemathApi.Models;

// TODO: Student class documentation.

/// <summary>
/// Represents an ordinary user as a student with the personal information and achievements.
/// </summary>
public class Student
{
    #region Properties

    /// <summary>
    /// The unique student identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The student's nickname.
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// The student's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The student's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The list of identifiers of problems solved by the student.
    /// </summary>
    public List<Guid> SolvedMathProblems { get; set; }

    /// <summary>
    /// The student's experience.
    /// </summary>
    public int Experience { get; set; }

    /// <summary>
    /// The number of correct answers to problems of each type.
    /// </summary>
    public Dictionary<MathProblemTypes, int> CorrectAnswers { get; set; } = new();

    /// <summary>
    /// The number of incorrect answers to problems of each type.
    /// </summary>
    public Dictionary<MathProblemTypes, int> IncorrectAnswers { get; set; } = new();

    /// <summary>
    /// The student's location.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// The student's about.
    /// </summary>
    public string About { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Student"/> class.
    /// </summary>
    public Student() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Student"/> class using an exist <see cref="StudentDto"/> class instance.
    /// </summary>
    /// <param name="dto">The Data Transfer Object from which the properties value will be taken.</param>
    public Student(StudentDto dto)
    {
        this.Id = dto.Id;
        this.Nickname = dto.Nickname;
        this.FirstName = dto.FirstName;
        this.LastName = dto.LastName;
        this.SolvedMathProblems = dto.SolvedMathProblems.ToList();
        this.Experience = dto.Experience;

        int[] enumValues = (int[])Enum.GetValues(typeof(MathProblemTypes));

        for (int ii = 0; ii < enumValues.Length; ii++)
        {
            this.CorrectAnswers.Add((MathProblemTypes)enumValues[ii], dto.CorrectAnswersOrder[ii]);
            this.IncorrectAnswers.Add((MathProblemTypes)enumValues[ii], dto.IncorrectAnswersOrder[ii]);
        }

        this.Location = dto.Location;
        this.About = dto.About;
    }

    #endregion
}
