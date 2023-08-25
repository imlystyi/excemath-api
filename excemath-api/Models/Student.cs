namespace excemathApi.Models;

/// <summary>
/// Represents an ordinary user as a student with the personal information and achievements.
/// </summary>
public class Student
{
    #region Properties

    /// <summary>
    /// Gets or sets the unique student identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the student's nickname.
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// Gets or sets the student's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the student's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the list of identifiers of math problems solved by the student.
    /// </summary>
    public List<Guid> SolvedMathProblems { get; set; }

    /// <summary>
    /// Gets or sets the student's experience.
    /// </summary>
    public int Experience { get; set; }

    /// <summary>
    /// Gets or sets the number of correct answers to math problems of each type.
    /// </summary>
    public Dictionary<MathProblemTypes, int> CorrectAnswers { get; set; } = new();

    /// <summary>
    /// Gets or sets the number of incorrect answers to math problems of each type.
    /// </summary>
    public Dictionary<MathProblemTypes, int> IncorrectAnswers { get; set; } = new();

    /// <summary>
    /// Gets or sets the student's location.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Gets or sets the student's about.
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
