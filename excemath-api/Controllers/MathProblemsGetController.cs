/* excemath - an app for preparing for math exams.
* Copyright (C) 2023 miu-miu enjoyers

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program. If not, see <https://www.gnu.org/licenses/>. */

using excemathApi.Data;
using excemathApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="MathProblemsApiDbContext"/>.
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
    /// Дозволяє клієнту отримати список ідентифікаторів об'єктів класу <see cref="MathProblem"/> за вказаним видом.
    /// </summary>
    /// <param name="kind">Вид математичної задачі.</param>
    /// <returns>
    /// Якщо знайдено принаймні один об'єкт, то список знайдених ідентифікаторів як <see cref="List{T}"/> з <see cref="int"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>); інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.
    /// </returns>
    [HttpGet]
    [Route("kind_list")]
    public async Task<IActionResult> GetMathProblemsIdsList([FromQuery] MathProblemKinds kind)
    {
        List<int> ids = await Task.Run(() => _dbContext.MathProblems.Where(p => p.Kind == kind).Select(p => p.Id).ToListAsync());

        if (!ids.Any())
            return NotFound();

        return Ok(ids);
    }

#nullable enable

    /// <summary>
    /// Дозволяє клієнту отримати конкретний об'єкт класу <see cref="MathProblem"/> за вказаним ідентифікатором.
    /// </summary>
    /// <param name="id">Ідентифікатор математичної задачі.</param>
    /// <returns>
    /// Якщо об'єкт знайдено, його як <see cref="MathProblem"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>); інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.
    /// </returns>
    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> GetMathProblem([FromQuery] int id)
    {
        MathProblem? mathProblem = await _dbContext.MathProblems.FindAsync(id);

        if (mathProblem is null)
            return NotFound();

        return Ok(mathProblem);
    }

    #endregion
}
