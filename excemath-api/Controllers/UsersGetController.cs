#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="UsersApiDbContext"/>, який дозволяє <b>отримувати</b> дані.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersGetController : Controller
{
    #region Поля

    private readonly UsersApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UsersGetController"/>, використовуючи вказаний контекст бази даних. 
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    public UsersGetController(UsersApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Методи

    /// <summary>
    /// Дозволяє клієнту отримати рейтинговий список користувачів.
    /// </summary>
    /// <remarks>
    /// Рейтинг обчислюється наступним чином:    
    /// <code>double rating = ((User.RightAnswers + User.WrongAnswers) > 0) ? ((double)User.RightAnswers / (User.RightAnswers + User.WrongAnswers)) : 0;</code>
    /// </remarks>
    /// <returns>
    /// Рейтинговий список користувачів як список <see cref="List{T}"/> з <see cref="UserRating"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
    /// </returns>
    [HttpGet]
    [Route("rating_list")]
    public async Task<IActionResult> GetRatingList()
    {
        List<User> users = await _dbContext.Users.ToListAsync();

        var ratingList = users.Select(u =>
        {
            double rating = ((u.RightAnswers + u.WrongAnswers) > 0) ? ((double)u.RightAnswers * 100 / (u.RightAnswers + u.WrongAnswers)) : 0;

            return new UserRating
            {
                Nickname = u.Nickname,
                Rating = rating
            };
        }).OrderByDescending(u => u.Rating).Select(u => Math.Round(u.Rating, 2)).ToList();

        return Ok(ratingList);
    }

    /// <summary>
    /// Дозволяє клієнту отримати конкретного користувача за вказаним псевдонімом.
    /// </summary>
    /// <param name="nickname">Псевдонім користувача.</param>
    /// <returns>
    /// Якщо користувача з таким псевдонімом знайдено, конкретного користувача як <see cref="UserGetRequest"/> (інтегрованого у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
    /// </returns>
    [HttpGet]
    [Route("nickname/{nickname}")]
    public async Task<IActionResult> GetUser([FromRoute] string nickname)
    {
        User? user = await _dbContext.Users.FindAsync(nickname);

        if (user is null)
            return NotFound();

        return Ok(new UserGetRequest
        {
            Nickname = user.Nickname,
            RightAnswers = user.RightAnswers,
            WrongAnswers = user.WrongAnswers
        });
    }

    #endregion
}
