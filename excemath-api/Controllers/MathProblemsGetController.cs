#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="MathProblemsApiDbContext"/>, який дозволяє <b>отримувати</b> дані.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MathProblemsGetController : Controller
{
    #region Поля

    private readonly MathProblemsApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="MathProblemsGetController"/>, використовуючи вказаний контекст бази даних.
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    public MathProblemsGetController(MathProblemsApiDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Методи

    /// <summary>
    /// Дозволяє клієнту отримати список ідентифікаторів математичних проблем за вказаним видом.
    /// </summary>
    /// <param name="kind">Вид математичної проблеми.</param>
    /// <returns>
    /// Якщо за видом знайдено принаймні одну математичну проблему, то список знайдених ідентифікаторів математичних проблем як <see cref="List{T}"/> з <see cref="int"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("kind_list/{kind}")]
    public async Task<IActionResult> GetMathProblemsIdsList([FromRoute] MathProblemKinds kind)
    {
        List<int> ids = await Task.Run(() => _dbContext.MathProblems.Where(p => p.Kind == kind).Select(p => p.Id).ToListAsync());

        if (!ids.Any())
            return NotFound();

        return Ok(ids);
    }

    /// <summary>
    /// Дозволяє клієнту отримати конкретну математичну проблему за вказаним ідентифікатором.
    /// </summary>
    /// <param name="id">Ідентифікатор математичної проблеми.</param>
    /// <returns>
    /// Якщо математичну проблему з таким ідентифікатором знайдено, конкретну математичну проблему як <see cref="MathProblem"/> (інтегрована у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetMathProblem([FromRoute] int id)
    {
        MathProblem? mathProblem = await _dbContext.MathProblems.FindAsync(id);

        if (mathProblem is null)
            return NotFound();

        return Ok(mathProblem);
    }

    #endregion
}
