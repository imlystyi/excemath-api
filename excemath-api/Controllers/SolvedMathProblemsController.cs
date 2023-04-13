#nullable enable

#region Usings-частина

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

#endregion

namespace excemathApi.Controllers
{
    /// <summary>
    /// Представляє контролер для контексту бази даних <see cref="SolvedMathProblemsApiDbContext"/>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SolvedMathProblemsController: Controller
    {
        #region Поля

        /// <summary>
        /// Контекст бази даних контролера.
        /// </summary>
        private readonly SolvedMathProblemsApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="SolvedMathProblemsController"/>, використовуючи зазначений контекст бази даних.
        /// </summary>
        /// <param name="dbContext">Контекст бази даних.</param>
        public SolvedMathProblemsController(SolvedMathProblemsApiDbContext dbContext) => _dbContext = dbContext;

        #endregion

        #region HttpGet-методи

        /// <summary>
        /// Дозволяє отримати всі розв'язані математичні проблеми у вигляді списку.
        /// </summary>
        /// <returns>
        /// Список всіх розв'язаних математичних проблем як список <see cref="List{SolvedMathProblem}"/> з елементів класу <see cref="SolvedMathProblem"/>..
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSolvedMathProblems() => Ok(await _dbContext.SolvedMathProblems.ToListAsync());

        /// <summary>
        /// Дозволяє отримати конкретну розв'язану математичну проблему за її ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор розв'язаної математичної проблеми.</param>
        /// <returns>
        /// Конкретну розв'язану математичну проблему за її ідентифікатором як <see cref="SolvedMathProblem"/>.
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetSolvedMathProblem([FromRoute] int id)
        {
            SolvedMathProblem? solvetMathProblem = await _dbContext.SolvedMathProblems.FindAsync(id);

            if (solvetMathProblem is null)
                return NotFound();

            return Ok(solvetMathProblem);
        }

        #endregion
    }
}
