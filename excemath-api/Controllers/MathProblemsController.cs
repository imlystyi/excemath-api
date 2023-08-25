using excemathApi.Contexts;
using excemathApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Controllers;

// TODO: MathProblemsController class documentation.

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/math_problems")]
public class MathProblemsController : Controller
{
    #region Fields

    private readonly MathProblemsDbContext _dbContext;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public MathProblemsController(MathProblemsDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region GET methods

    #nullable enable 

    /// <summary>
    /// Asynchronously finds a <see cref="MathProblemDto"/> entity in the database by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the sought entity.</param>
    /// <returns>If the entity was found, the <see cref="OkObjectResult"/> with the new <see cref="MathProblem"/> object (created from the DTO) as a content; otherwise, <see cref="NotFoundResult"/>.</returns>
    [HttpGet]
    [Route("get/find_one")]
    public async Task<IActionResult> FindOne([FromQuery] Guid id)
    {
        MathProblemDto? mathProblemDto = await _dbContext.MathProblems.FindAsync(id);

        if (mathProblemDto is null)
            return NotFound();

        return Ok(new MathProblem(mathProblemDto));
    }

    #nullable restore

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="solvedIds"></param>
    /// <returns></returns>

    [HttpGet]
    [Route("get/get_ids")]
    public async Task<IActionResult> GetIds([FromQuery] MathProblemTypes type, [FromQuery] List<Guid> solvedIds)
    {
        List<Guid> ids = await _dbContext.MathProblems
            .Where(mp => mp.Type == type && !solvedIds.Contains(mp.Id))
            .Select(mp => mp.Id)
            .ToListAsync();

        if (!ids.Any())
            return NotFound();

        return Ok(ids);
    }

    #endregion

    #region POST methods

    /// <summary>
    /// Asynchronously adds a <see cref="MathProblem"/> object as an entity to the database.
    /// </summary>
    /// <param name="mathProblem">An object to be added.</param>
    /// <returns><see cref="OkResult"/> if the entity was added successfully; otherwise, <see cref="ConflictResult"/>.</returns>
    [HttpPost]
    [Route("post/add")]
    public async Task<IActionResult> Add([FromBody] MathProblem mathProblem)
    {
        MathProblemDto mathProblemDto = mathProblem.ToDto();

        _ = await _dbContext.AddAsync(mathProblemDto);
        int entries = await _dbContext.SaveChangesAsync();

        if (entries > 0)
            return Ok();

        return Conflict();
    }

    #endregion
}
