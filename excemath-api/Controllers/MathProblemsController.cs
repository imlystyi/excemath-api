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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static excemathApi.Controllers.ControllerResults;

namespace excemathApi.Controllers;

/// <summary>
/// Represents a controller that interacts with entities of the <see cref="MathProblem"/> and <see cref="MathProblemDto"/> types.
/// </summary>
[ApiController]
[Route("api/math_problems")]
public class MathProblemsController : Controller
{
    #region Fields

    private const string _MATH_PROBLEMS_CONTROLLER_ERROR_HEADER = "MathProblemsController error ";

    private readonly MathProblemsDbContext _dbContext;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="MathProblemsController"/> class with the specified database context.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    public MathProblemsController(MathProblemsDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region GET methods

    #nullable enable

    /// <summary>
    /// Asynchronously finds a math problem in the database by its identifier and returns it.
    /// </summary>
    /// <param name="id">Math problem identifier.</param>
    /// <returns>If the math problem is found, an <see cref="OkObjectResult"/> with a <see cref="MathProblem"/> object as a content; otherwise, <see cref="NotFoundResult"/>.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpGet]
    [Route("get/find_one")]
    public async Task<IActionResult> FindOneAsync([FromQuery] Guid id)
    {
        try
        {
            MathProblemDto? dto = await _dbContext.MathProblems.FindAsync(id);

            return dto is null
                ? NotFound()
                : Ok(new MathProblem(dto));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #nullable restore

    /// <summary>
    /// Asynchronously finds a list of math problem identifiers of the specified type in the database, excluding solved math problem identifiers, and returns it.
    /// </summary>
    /// <param name="type">Sought math problem type.</param>
    /// <param name="solvedIds">Solved math problem identifiers list.</param>
    /// <returns>An <see cref="OkObjectResult"/> with list of identifiers as <see cref="List{T}"/> from <see cref="Guid"/> objects as content if at least one identifier is found; otherwise, <see cref="NotFoundResult"/>.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpGet]
    [Route("get/find_ids")]
    public async Task<IActionResult> FindIdsAsync([FromQuery] MathProblemTypes type, [FromQuery] List<Guid> solvedIds)
    {
        try
        {
            List<Guid> ids = await _dbContext.MathProblems
                .Where(pp => pp.Type == type && !solvedIds.Contains(pp.Id))
                .Select(pp => pp.Id)
                .ToListAsync();

            return !ids.Any()
                ? NotFound()
                : Ok(ids);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region POST methods

    /// <summary>
    /// Asynchronously adds a math problem to the database.
    /// </summary>
    /// <param name="problem">The object to convert to a Data Transfer Object and add.</param>
    /// <returns>An <see cref="OkResult"/> if the math problem is added successfully; otherwise, if there are no entries while saving changes to the database, <see cref="InternalServerErrorObjectResult"/> with the error text as content.<br>
    /// If an exception occurs during operations, returns <see cref="InternalServerErrorObjectResult"/> with this exception as content.</br></returns>
    [HttpPost]
    [Route("post/add")]
    public async Task<IActionResult> AddAsync([FromBody] MathProblem problem)
    {
        try
        {
            MathProblemDto dto = problem.ToDto();

            _ = await _dbContext.AddAsync(dto);
            int entries = await _dbContext.SaveChangesAsync();

            return entries > 0
                ? Ok()
                : InternalServerError(_MATH_PROBLEMS_CONTROLLER_ERROR_HEADER +
                                      $"({nameof(AddAsync)} method): no \"{nameof(entries)}\" while saving changes.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion
}
