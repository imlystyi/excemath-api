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

using excemathApi.Contexts;
using excemathApi.Models;
using excemathApi.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static excemathApi.Controllers.ControllerResults;

namespace excemathApi.Controllers;

/// <summary>
/// Represents a controller that interacts with entities of the <see cref="Student"/> and <see cref="StudentDto"/> types.
/// </summary>
[ApiController]
[Route("api/students")]
public class StudentsController : Controller
{
    #region Fields

    /// <summary>
    /// Number of students in the first list.
    /// </summary>
    public const int TOP_LIST_COUNT = 10;

    private const string _STUDENTS_CONTROLLER_ERROR_HEADER = "StudentsController error ";

    private readonly StudentsDbContext _dbContext;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="StudentsController"/> class with the specified database context.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    public StudentsController(StudentsDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #nullable enable

    #region GET methods

    /// <summary>
    /// Asynchronously finds the student in the database by their nickname and returns they.
    /// </summary>
    /// <param name="nickname">Student identifier.</param>
    /// <returns>If the student is found, an <see cref="OkObjectResult"/> with a <see cref="Student"/> object as content; otherwise, <see cref="NotFoundResult"/>.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpGet]
    [Route("get/find_one")]
    public async Task<IActionResult> FindOneAsync([FromQuery] string nickname)
    {
        try
        {
            StudentDto? dto = await _dbContext.Students.FirstOrDefaultAsync(ss => ss.Nickname == nickname);

            return dto is null
                ? NotFound()
                : Ok(new Student(dto));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously finds the list of students in the database by nickname (as a substring of nickname) and returns it.
    /// </summary>
    /// <param name="nickname">Nickname (nickname substring) of the sought students in the list.</param>
    /// <returns>An <see cref="OkObjectResult"/> with list of students as <see cref="List{T}"/> from <see cref="Student"/> objects as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpGet]
    [Route("get/find_list")]
    public async Task<IActionResult> FindListAsync([FromQuery] string nickname)
    {
        try
        {
            List<StudentDto> dtos = await _dbContext.Students.Where(ss => ss.Nickname.Contains(nickname)).ToListAsync();

            return Ok(dtos.ConvertAll(dto => new Student(dto)));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously finds a top list of students ordered by their experience and returns it.
    /// </summary>
    /// <returns>An <see cref="OkObjectResult"/> with list of students as <see cref="List{T}"/> from <see cref="Student"/> objects with <see cref="TOP_LIST_COUNT"/> elements count as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpGet]
    [Route("get/find_top_list")]
    public async Task<IActionResult> FindTopListAsync()
    {
        try
        {
            List<StudentDto> dtos = await _dbContext.Students.OrderByDescending(ss => ss.Experience).Take(TOP_LIST_COUNT).ToListAsync();

            return Ok(dtos.ConvertAll(dto => new Student(dto)));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region POST methods

    /// <summary>
    /// Asynchronously adds a student with the specified identifier and nickname to the database.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="nickname">Student nickname.</param>
    /// <returns>An <see cref="OkResult"/> if the student is added successfully; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as contents.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPost]
    [Route("post/add")]
    public async Task<IActionResult> AddAsync([FromQuery] Guid id, [FromQuery] string nickname)
    {
        try
        {
            StudentDto dto = new()
            {
                Id = id,
                Nickname = nickname
            };

            _ = await _dbContext.Students.AddAsync(dto);
            int entries = await _dbContext.SaveChangesAsync();

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(AddAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region PUT methods

    /// <summary>
    /// Asynchronously adds the specified solved math problem to the achievements of the student with the specified identifier.
    /// </summary>
    /// <param name="studentId">Student identifier.</param>
    /// <param name="mathProblemId">Solved math problem identifier.</param>
    /// <param name="difficulty">Solved math problem difficulty.</param>
    /// <param name="type">Solved math problem type.</param>
    /// <returns>An <see cref="OkResult"/> if the solved math problem is added successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/add_solved_math_problems")]
    public async Task<IActionResult> AddSolvedMathProblemsAsync([FromQuery] Guid studentId, [FromQuery] Guid mathProblemId,
        [FromQuery] int difficulty, [FromQuery] MathProblemTypes type)
    {
        try
        {
            StudentDto? dto = await _dbContext.Students.FindAsync(studentId);

            if (dto is null)
                return NotFound();

            dto.SolvedMathProblems.Add(mathProblemId);
            dto.Experience += difficulty;
            dto.CorrectAnswersOrder[(int)type]++;

            int entries = await _dbContext.SaveChangesAsync();

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(AddSolvedMathProblemsAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously adds the incorrect answer to the achievements of the student with the specified identifier.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="type">The type of math problem that was answered incorrectly.</param>
    /// <returns>An <see cref="OkResult"/> if the incorrect answer is added successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/add_incorrect_answer")]
    public async Task<IActionResult> AddIncorrectAnswerAsync([FromQuery] Guid id, [FromQuery] MathProblemTypes type)
    {
        try
        {
            StudentDto? dto = await _dbContext.Students.FindAsync(id);

            if (dto is null)
                return NotFound();

            dto.IncorrectAnswersOrder[(int)type]++;
            int entries = await _dbContext.SaveChangesAsync();

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(AddIncorrectAnswerAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously updates the nickname of the student with the specified identifier.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="nickname">New nickname.</param>
    /// <returns>An <see cref="OkResult"/> if the nickname is updated successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/update_nickname")]
    public async Task<IActionResult> UpdateNicknameAsync([FromQuery] Guid id, [FromQuery] string nickname)
    {
        try
        {
            StudentDto? dto = await _dbContext.Students.FindAsync(id);

            if (dto is null)
                return NotFound();

            StringValidator validator = new(nameof(StudentDto.Nickname));
            ValidationResult result = await validator.ValidateAsync(nickname);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            dto.Nickname = nickname;
            int entries = await _dbContext.SaveChangesAsync();

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(UpdateNicknameAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously updates the first name of the student with the specified identifier.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="firstName">New first name.</param>
    /// <returns>An <see cref="OkResult"/> if the first name is updated successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/update_first_name")]
    public async Task<IActionResult> UpdateFirstNameAsync([FromQuery] Guid id, [FromQuery] string firstName)
    {
        try
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

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(UpdateFirstNameAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously updates the last name of the student with the specified identifier.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="lastName">New last name.</param>
    /// <returns>An <see cref="OkResult"/> if the last name is updated successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/update_last_name")]
    public async Task<IActionResult> UpdateLastNameAsync([FromQuery] Guid id, [FromQuery] string lastName)
    {
        try
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

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(UpdateLastNameAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously updates the location of the student with the specified identifier.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="location">New location.</param>
    /// <returns>An <see cref="OkResult"/> if the location is updated successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/update_location")]
    public async Task<IActionResult> UpdateLocationAsync([FromQuery] Guid id, [FromQuery] string location)
    {
        try
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

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(UpdateLocationAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// Asynchronously updates the about information of the student with the specified identifier.
    /// </summary>
    /// <param name="id">Student identifier.</param>
    /// <param name="about">New about information.</param>
    /// <returns>An <see cref="OkResult"/> if the about information is updated successfully; otherwise, if the student is not found, <see cref="NotFoundResult"/>; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPut]
    [Route("put/update_about")]
    public async Task<IActionResult> UpdateAbout([FromQuery] Guid id, [FromQuery] string about)
    {
        try
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

            return entries > 0
                ? Ok()
                : InternalServerError(_STUDENTS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(UpdateAbout)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion
}
