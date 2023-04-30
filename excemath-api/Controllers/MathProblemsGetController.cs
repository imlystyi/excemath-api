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

    // Контекст бази даних контролера.
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

    // TODO: видалити #2
    ///// <summary>
    ///// Дозволяє отримати всі математичні проблеми у вигляді списку.
    ///// </summary>
    ///// <returns>
    ///// Список математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
    ///// </returns>
    //[HttpGet]
    //[Route("all")]
    //public async Task<IActionResult> GetAllMathProblems() => Ok(await _dbContext.MathProblems.ToListAsync());

    /// <summary>
    /// Дозволяє клієнту отримати список математичних проблем за вказаним списком ідентифікаторів.
    /// </summary>
    /// <param name="ids">Список ідентифікаторів математичних проблем.</param>
    /// <returns>
    /// Якщо за ідентифікатором знайдено принаймні одну математичну проблему, то список знайдених математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("ids_list")]
    public async Task<IActionResult> GetMathProblemsList([FromRoute] List<int> ids)
    {
        List<MathProblem> mathProblems = await Task.Run(() => _dbContext.MathProblems.Where(p => ids.Contains(p.Id)).ToListAsync());

        if (!mathProblems.Any())
            return NotFound();

        return Ok(mathProblems);
    }

    /// <summary>
    /// Дозволяє клієнту отримати список математичних проблем за вказаним видом.
    /// </summary>
    /// <param name="kind">Вид математичної проблеми.</param>
    /// <returns>
    /// Якщо за видом знайдено принаймні одну математичну проблему, то список знайдених математичних проблем як <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("kinds_list/{kind}")]
    public async Task<IActionResult> GetMathProblemsList([FromRoute] MathProblemKinds kind)
    {
        List<MathProblem> mathProblems = await Task.Run(() => _dbContext.MathProblems.Where(p => p.Kind == kind).ToListAsync());

        if (!mathProblems.Any())
            return NotFound();

        return Ok(mathProblems);
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
