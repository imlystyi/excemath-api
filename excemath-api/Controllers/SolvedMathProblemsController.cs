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
        /// Дозволяє отримати всі математичні проблеми у вигляді списку.
        /// </summary>
        /// <returns>
        /// Список математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/>..
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSolvetMathProblems() => Ok(await _dbContext.SolvedMathProblems.ToListAsync());

        /// <summary>
        /// Дозволяє отримати конкретну математичну проблему за її ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор математичної проблеми.</param>
        /// <returns>
        /// Конкретну математичну проблему за її ідентифікатором як <see cref="MathProblem"/>.
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAllSolvetMathProblems([FromRoute] int id)
        {
            SolvedMathProblem? solvetMathProblem = await _dbContext.SolvedMathProblems.FindAsync(id);

            if (solvetMathProblem is null)
                return NotFound();

            return Ok(solvetMathProblem);
        }

        #endregion

    }
}
