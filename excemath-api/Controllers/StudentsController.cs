using excemathApi.Contexts;
using excemathApi.Models;
using excemathApi.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Controllers;

/// <summary>
///
/// </summary>
[ApiController]
[Route("api/students")]
public class StudentsController : Controller
{
    #region Fields

    private const int _TOP_LIST_COUNT = 10;

    private readonly StudentsDbContext _dbContext;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public StudentsController(StudentsDbContext dbContext) => _dbContext = dbContext;

    #endregion

#nullable enable

    #region GET methods

    [HttpGet]
    [Route("find_one")]
    public async Task<IActionResult> FindOne([FromQuery] string nickname)
    {
        StudentDto? dto = await _dbContext.Students.FirstOrDefaultAsync(ss => ss.Nickname == nickname);

        return (dto is null) ? NotFound() : Ok(new Student(dto));
    }

    [HttpGet]
    [Route("find_list")]
    public async Task<IActionResult> FindList([FromQuery] string nickname)
    {
        List<StudentDto> dtos = await _dbContext.Students.Where(ss => ss.Nickname.Contains(nickname)).ToListAsync();

        return Ok(dtos.ConvertAll(dto => new Student(dto)));
    }

    [HttpGet]
    [Route("get_top_list")]
    public async Task<IActionResult> GetTopList()
    {
        List<StudentDto> dtos = await _dbContext.Students.OrderByDescending(ss => ss.Experience).Take(_TOP_LIST_COUNT).ToListAsync();

        return Ok(dtos.ConvertAll(dto => new Student(dto)));
    }

    #endregion

    #region POST methods

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromQuery] Guid id, [FromQuery] string nickname)
    {
        StudentDto dto = new()
        {
            Id = id,
            Nickname = nickname
        };

        _ = await _dbContext.Students.AddAsync(dto);
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    #endregion

    #region PUT methods

    [HttpPut]
    [Route("increase_solved_math_problems")]
    public async Task<IActionResult> IncreaseSolvedMathProblems([FromQuery] Guid studentId, [FromQuery] Guid mathProblemId, [FromQuery] int difficulty, [FromQuery] MathProblemTypes type)    
    {
        StudentDto? dto = await _dbContext.Students.FindAsync(studentId);

        if (dto is null)
            return NotFound();

        dto.SolvedMathProblems.Add(mathProblemId);
        dto.Experience += difficulty;
        dto.CorrectAnswersOrder[(int)type]++;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    [HttpPut]
    [Route("increase_incorrect_answers")]
    public async Task<IActionResult> IncreaseIncorrectAnswers([FromQuery] Guid id, [FromQuery] MathProblemTypes type)
    {
        StudentDto? dto = await _dbContext.Students.FindAsync(id);

        if (dto is null)
            return NotFound();

        dto.IncorrectAnswersOrder[(int)type]++;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    [HttpPut]
    [Route("update_nickname")]
    public async Task<IActionResult> UpdateNickname([FromQuery] Guid id, [FromQuery] string nickname)
    {
        StudentDto? student = await _dbContext.Students.FindAsync(id);

        if (student is null)
            return NotFound();

        StringValidator validator = new(nameof(StudentDto.Nickname));
        ValidationResult result = await validator.ValidateAsync(nickname);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        student.Nickname = nickname;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    [HttpPut]
    [Route("update_first_name")]
    public async Task<IActionResult> UpdateFirstName([FromQuery] Guid id, [FromQuery] string firstName)
    {
        StudentDto? student = await _dbContext.Students.FindAsync(id);

        if (student is null)
            return NotFound();

        StringValidator validator = new(nameof(StudentDto.FirstName));
        ValidationResult result = await validator.ValidateAsync(firstName);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        student.FirstName = firstName;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    [HttpPut]
    [Route("update_last_name")]
    public async Task<IActionResult> UpdateLastName([FromQuery] Guid id, [FromQuery] string lastName)
    {
        StudentDto? student = await _dbContext.Students.FindAsync(id);

        if (student is null)
            return NotFound();

        StringValidator validator = new(nameof(StudentDto.LastName));
        ValidationResult result = await validator.ValidateAsync(lastName);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        student.LastName = lastName;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    [HttpPut]
    [Route("update_location")]
    public async Task<IActionResult> UpdateLocation([FromQuery] Guid id, [FromQuery] string location)
    {
        StudentDto? student = await _dbContext.Students.FindAsync(id);

        if (student is null)
            return NotFound();
        StringValidator validator = new(nameof(StudentDto.Location));
        ValidationResult result = await validator.ValidateAsync(location);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        student.Location = location;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    [HttpPut]
    [Route("update_about")]
    public async Task<IActionResult> UpdateAbout([FromQuery] Guid id, [FromQuery] string about)
    {
        StudentDto? student = await _dbContext.Students.FindAsync(id);

        if (student is null)
            return NotFound();
        StringValidator validator = new(nameof(StudentDto.About));
        ValidationResult result = await validator.ValidateAsync(about);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        student.About = about;
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    #endregion
}
