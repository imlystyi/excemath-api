// Зробити XML-документацію.

#region Usings-частина

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

#endregion

namespace excemathApi.Controllers
{
    /// <summary>
    /// Представляє контролер для контексту математичної проблеми <see cref="MathProblemsApiDbContext"/>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MathProblemsController : Controller
    {
        #region Поля

        /// <summary>
        /// 
        /// </summary>
        private readonly MathProblemsApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public MathProblemsController(MathProblemsApiDbContext dbContext) => _dbContext = dbContext;

        #endregion

        #region HttpGet-методи

        /// <summary>
        /// Дозволяє отримати всі математичні проблеми у вигляді списку.
        /// </summary>
        /// <returns>
        /// Список математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/>..
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMathProblems() => Ok(await _dbContext.MathProblems.ToListAsync());

        /// <summary>
        /// Дозволяє отримати конкретну математичну проблему за її ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор математичної проблеми.</param>
        /// <returns>
        /// Конкретну математичну проблему за її ідентифікатором як <see cref="MathProblem"/>.
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMathProblem([FromRoute] int id)
        {
            MathProblem? mathProblem = await _dbContext.MathProblems.FindAsync(id);

            if (mathProblem is null)
                return NotFound();
            

            return Ok(mathProblem);
        }

        /// <summary>
        /// Дозволяє отримати список математичних проблем за вказаними ідентифікаторами.
        /// </summary>
        /// <param name="ids">Масив ідентифікаторів, за якими варто знаходити математичні проблеми.</param>
        /// <returns>
        /// Список математичних проблем як список <see cref="List{MathProblem}"/> з елементів класу <see cref="MathProblem"/>.
        /// </returns>
        [HttpGet]
        [Route("{ids}")]
        public async Task<IActionResult> GetMathProblemsList([FromRoute] int[] ids) // Не працює!
        {
            List<MathProblem> mathProblems = await Task.Run(() => _dbContext.MathProblems.Where(p => ids.Contains(p.Id)).ToList());

            if (mathProblems is null)
                return NotFound();
            
            
            return Ok(mathProblems);
        }

        #endregion

        #region HttpPost-методи

        // Методи в цьому регіоні призначені виключно для відлагодження!

        // Додає математичну проблему до бази даних.
        [HttpPost]
        public async Task<IActionResult>AddMathProblem(AddMathProblemRequest addMathProblemRequest)
        {
            MathProblem last = _dbContext.MathProblems.LastOrDefault() ?? new MathProblem()
            {
                Id = 0,
                Kind = MathProblemKinds.DefiniteIntegrals,
                Question = "empty",
                Answer = "empty"
            };

            MathProblem mathProblem = new()
            {
                Id = last.Id + 1,
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
