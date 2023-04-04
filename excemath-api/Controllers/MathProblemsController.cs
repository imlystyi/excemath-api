#region Usings-частина
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;
#endregion

namespace excemathApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathProblemsController : Controller
    {
        #region Поля
        private readonly MathProblemsAPIDbContext _dbContext;
        #endregion

        #region Конструктори
        public MathProblemsController(MathProblemsAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region HttpGet-методи
        /// <summary>
        /// Дозволяє отримати всі математичні проблеми у вигляді списку.
        /// </summary>
        /// <returns>
        /// Всі математичні проблеми у вигляді списку.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMathProblems() => Ok(await _dbContext.MathProblems.ToListAsync());

        /// <summary>
        /// Дозволяє отримати конкретну математичну проблему за її ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор математичної проблеми.</param>
        /// <returns>
        /// Конкретну математичну проблему за її ідентифікатором.
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMathProblem([FromRoute] int id)
        {
            var mathProblem = await _dbContext.MathProblems.FindAsync(id);

            if (mathProblem is null)
            {
                return NotFound();
            }

            return Ok(mathProblem);
        }
        #endregion

        #region HttpPost-методи
        [HttpPost]
        public async Task<IActionResult>AddMathProblem(AddMathProblemRequest addMathProblemRequest)
        {
            MathProblem last = _dbContext.MathProblems.LastOrDefault() ?? new MathProblem()
            {
                Id = 0,
                Kind = ProblemKinds.DefiniteIntegrals,
                Question = "empty",
                Answer = "empty"
            };

            MathProblem mathProblem = new()
            {
                Id = last.Id + 1, // Реалізувати збільшення id на 1.
                Kind = addMathProblemRequest.Kind,
                Question = addMathProblemRequest.Question,
                Answer = addMathProblemRequest.Answer
            };

            await _dbContext.AddAsync(mathProblem);
            await _dbContext.SaveChangesAsync();

            return Ok(mathProblem);
        }
        #endregion
    }
}
