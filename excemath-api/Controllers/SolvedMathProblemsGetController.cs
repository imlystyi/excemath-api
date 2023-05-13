using excemathApi.Data;
using excemathApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /// Створює екземпляр класу <see cref="SolvedMathProblemsGetController"/>, використовуючи вказаний контекст бази даних.
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    public SolvedMathProblemsGetController(SolvedMathProblemsApiDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Методи

    /// <summary>
    /// Дозволяє клієнту отримати список ідентифікаторів об'єктів класу <see cref="SolvedMathProblem"/> за вказаним видом.
    /// </summary>
    /// <param name="kind">Вид розв'язаної математичної задачі.</param>
    /// <returns>
    /// Якщо знайдено принаймні один об'єкт, то список знайдених ідентифікаторів як <see cref="List{T}"/> з <see cref="int"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>); інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.
    /// </returns>
    [HttpGet]
    [Route("kinds_list")]
    public async Task<IActionResult> GetSolvedMathProblemsList([FromQuery] MathProblemKinds kind)
    {
        List<SolvedMathProblem> solvedMathProblems = await Task.Run(() => _dbContext.SolvedMathProblems.Where(p => p.Kind == kind).ToListAsync());

        if (!solvedMathProblems.Any())
            return NotFound();

        return Ok(solvedMathProblems);
    }

#nullable enable

    /// <summary>
    /// Дозволяє клієнту отримати конкретний об'єкт класу <see cref="SolvedMathProblem"/> за вказаним ідентифікатором.
    /// </summary>
    /// <param name="id">Ідентифікатор розв'язаної математичної задачі.</param>
    /// <returns>
    /// Якщо об'єкт знайдено, його як <see cref="SolvedMathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>); інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.
    /// </returns>
    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> GetSolvedMathProblem([FromQuery] int id)
    {
        SolvedMathProblem? solvedMathProblem = await _dbContext.SolvedMathProblems.FindAsync(id);

        if (solvedMathProblem is null)
            return NotFound();

        return Ok(solvedMathProblem);
    }

    #endregion
}
