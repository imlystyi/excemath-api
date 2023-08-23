namespace excemathApi.Models;

/// <summary>
/// 
/// </summary>
public class Student
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<Guid> SolvedMathProblems { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Experience { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<MathProblemTypes, int> CorrectAnswers { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<MathProblemTypes, int> IncorrectAnswers { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string About { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    public Student() { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
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

    #region Methods

    /// <summary>
    /// Creates a <see cref="StudentDto"/> from a current <see cref="Student"/> class instance.
    /// </summary>
    /// <returns> A <see cref="StudentDto"/> which properties match a current <see cref="Student"/> class instance.</returns>
    public StudentDto ToDto() => new()
    {
        Id = this.Id,
        Nickname = this.Nickname,
        FirstName = this.FirstName,
        LastName = this.LastName,
        SolvedMathProblems = this.SolvedMathProblems,
        Experience = this.Experience,
        CorrectAnswersOrder = this.CorrectAnswers.Select(ca => ca.Value).ToList(),
        IncorrectAnswersOrder = this.IncorrectAnswers.Select(ia => ia.Value).ToList(),
        Location = this.Location,
        About = this.About
    };

    #endregion
}
