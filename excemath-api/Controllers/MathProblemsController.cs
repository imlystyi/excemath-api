#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

namespace excemathApi.Controllers
{
    /// <summary>
    /// Представляє контролер для контексту бази даних <see cref="MathProblemsApiDbContext"/>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MathProblemsController : Controller
    {
        #region Поля

        // Контекст бази даних контролера.
        private readonly MathProblemsApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="MathProblemsController"/>, використовуючи вказаний контекст бази даних.
        /// </summary>
        /// <param name="dbContext">Контекст бази даних.</param>
        public MathProblemsController(MathProblemsApiDbContext dbContext) => _dbContext = dbContext;

        #endregion

        #region Методи запитів отримання

        /// <summary>
        /// Дозволяє отримати всі математичні проблеми у вигляді списку.
        /// </summary>
        /// <returns>
        /// Список математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
        /// </returns>
        [HttpGet]
        [Route("get/all")]
        public async Task<IActionResult> GetAllMathProblems() => Ok(await _dbContext.MathProblems.ToListAsync());

        /// <summary>
        /// Дозволяє отримати список математичних проблем за вказаним списком ідентифікаторів.
        /// </summary>
        /// <param name="ids">Список ідентифікаторів математичних проблем.</param>
        /// <returns>
        /// Якщо за ідентифікатором знайдено принаймні одну математичну проблему, то список знайдених математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
        /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
        /// </returns>
        [HttpGet]
        [Route("get/ids_list")]
        public async Task<IActionResult> GetMathProblemsList([FromQuery] List<int> ids)
        {
            List<MathProblem> mathProblems = await Task.Run(() => _dbContext.MathProblems.Where(p => ids.Contains(p.Id)).ToListAsync());

            if (!mathProblems.Any())
                return NotFound("За вказаними ідентифікаторами не знайдено жодної математичної проблеми.");

            return Ok(mathProblems);
        }

        /// <summary>
        /// Дозволяє отримати список математичних проблем за вказаним видом.
        /// </summary>
        /// <param name="kind">Вид математичної проблеми.</param>
        /// <returns>
        /// Якщо за видом знайдено принаймні одну математичну проблему, то список знайдених математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
        /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
        /// </returns>
        [HttpGet]
        [Route("get/kinds_list/{kind}")]
        public async Task<IActionResult> GetMathProblemsList([FromRoute] MathProblemKinds kind)
        {
            List<MathProblem> mathProblems = await Task.Run(() => _dbContext.MathProblems.Where(p => p.Kind == kind).ToListAsync());

            if (!mathProblems.Any())
                return NotFound("За вказаним видом не знайдено жодної математичної проблеми.");

            return Ok(mathProblems);
        }

        /// <summary>
        /// Дозволяє отримати конкретну математичну проблему за вказаним ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор математичної проблеми.</param>
        /// <returns>
        /// Якщо математичну проблему з таким ідентифікатором знайдено, конкретну математичну проблему як <see cref="MathProblem"/> (інтегрована у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
        /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
        /// </returns>
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetMathProblem([FromRoute] int id)
        {
            MathProblem? mathProblem = await _dbContext.MathProblems.FindAsync(id);

            if (mathProblem is null)
                return NotFound($"Математичну проблему з ідентифікатором '{id}' не знайдено.");

            return Ok(mathProblem);
        }

        #endregion
    }
}
