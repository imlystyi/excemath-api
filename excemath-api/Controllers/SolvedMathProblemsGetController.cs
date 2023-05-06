#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="SolvedMathProblemsApiDbContext"/>.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SolvedMathProblemsGetController : Controller
{
    #region Поля

    private readonly SolvedMathProblemsApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="SolvedMathProblemsGetController"/>, використовуючи зазначений контекст бази даних.
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    public SolvedMathProblemsGetController(SolvedMathProblemsApiDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Методи

    /// <summary>
    /// Дозволяє клієнту отримати список розв'язаних математичних проблем за вказаним списком ідентифікаторів.
    /// </summary>
    /// <param name="ids">Список ідентифікаторів розв'язаних математичних проблем.</param>
    /// <returns>
    /// Якщо за ідентифікатором знайдено принаймні одну розв'язану математичну проблему, то список знайдених розв'язаних математичних проблем як список <see cref="List{SolvedMathProblem}"/> з елементів класу <see cref="SolvedMathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("ids_list")]
    public async Task<IActionResult> GetSolvedMathProblemsList([FromQuery] List<int> ids)
    {
        List<SolvedMathProblem> solvedMathProblems = await Task.Run(() => _dbContext.SolvedMathProblems.Where(p => ids.Contains(p.Id)).ToListAsync());

        if (!solvedMathProblems.Any())
            return NotFound();

        return Ok(solvedMathProblems);
    }

    /// <summary>
    /// Дозволяє клієнту отримати список розв'язаних математичних проблем за вказаним видом.
    /// </summary>
    /// <param name="kind">Вид математичної проблеми.</param>
    /// <returns>
    /// Якщо за видом знайдено принаймні одну розв'язану математичну проблему, то список знайдених розв'язаних математичних проблем як список <see cref="List{SolvedMathProblem}"/> з елементів класу <see cref="SolvedMathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("kinds_list/{kind}")]
    public async Task<IActionResult> GetSolvedMathProblemsList([FromRoute] MathProblemKinds kind)
    {
        List<SolvedMathProblem> solvedMathProblems = await Task.Run(() => _dbContext.SolvedMathProblems.Where(p => p.Kind == kind).ToListAsync());

        if (!solvedMathProblems.Any())
            return NotFound();

        return Ok(solvedMathProblems);
    }

    /// <summary>
    /// Дозволяє клієнту отримати конкретну розв'язану математичну проблему за вказаним ідентифікатором.
    /// </summary>
    /// <param name="id">Ідентифікатор розв'язаної математичної проблеми.</param>
    /// <returns>
    /// Якщо розв'язану математичну проблему з таким ідентифікатором знайдено, конкретну розв'язану математичну проблему як <see cref="SolvedMathProblem"/> (інтегровану у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetSolvedMathProblem([FromRoute] int id)
    {
        SolvedMathProblem? solvedMathProblem = await _dbContext.SolvedMathProblems.FindAsync(id);

        if (solvedMathProblem is null)
            return NotFound();

        return Ok(solvedMathProblem);
    }

    #endregion
}
