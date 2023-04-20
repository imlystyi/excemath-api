// TODO: поліпшити логіку валідаторів.

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using excemathApi.Data;
using excemathApi.Models;
using excemathApi.Validators;

namespace excemathApi.Controllers
{
    /// <summary>
    /// Представляє контролер для контексту бази даних <see cref="UsersApiDbContext"/>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        #region Поля

        // Контекст бази даних контролера.
        private readonly UsersApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="UsersController"/>, використовуючи вказаний контекст бази даних. 
        /// </summary>
        /// <param name="dbContext">Контекст бази даних.</param>
        public UsersController(UsersApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Методи запитів отримання

        /// <summary>
        /// Дозволяє отримати всіх користувачів у вигляді списку.
        /// </summary>
        /// <remarks>
        /// Користувачі у списку повертаються як моделі запиту отримання <see cref="UserGetRequest"/>, які не мають властивості паролю (як <see cref="User.Password"/>).
        /// </remarks>
        /// <returns>
        /// Список користувачів як список <see cref="List{UserGetRequest}"/> з елементів класу <see cref="UserGetRequest"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
        /// </returns>
        [HttpGet]
        [Route("get/all")]
        public async Task<IActionResult> GetAllUsers() => Ok((await _dbContext.Users.ToListAsync()).ConvertAll(u => (UserGetRequest)u));

        /// <summary>
        /// Дозволяє отримати рейтинговий список всіх користувачів.
        /// </summary>
        /// <remarks>
        /// Користувачі у списку повертаються як анонимні типи, які мають такі члени, як псевдонім та рейтинг: <see langword="new"/> { <see cref="User.Nickname"/>, Rating }.<br>
        /// Рейтинг розраховується наступним чином:</br>        
        /// <code>double rating = ((User.RightAnswers + User.WrongAnswers) > 0) ? ((double)User.RightAnswers / (User.RightAnswers + User.WrongAnswers)) : 0;</code>
        /// </remarks>
        /// <returns>
        /// Список користувачів як список <see cref="List{GetUserRequest}"/> з елементів анонімного класу (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
        /// </returns>
        [HttpGet]
        [Route("get/rating_list")]
        public async Task<IActionResult> GetRatingList()
        {
            List<User> users = await _dbContext.Users.ToListAsync();

            var ratingList = users.Select(u =>
            {
                double rating = ((u.RightAnswers + u.WrongAnswers) > 0) ? ((double)u.RightAnswers / (u.RightAnswers + u.WrongAnswers)) : 0;

                return new { u.Nickname, Rating = rating };
            }).OrderByDescending(r => r.Rating).ToList();

            return Ok(ratingList);
        }

        /// <summary>
        /// Дозволяє отримати конкретного користувача за його псевдонімом.
        /// </summary>
        /// <remarks>
        /// Користувач повертається як модель запиту отримання <see cref="UserGetRequest"/>, яка не має властивості паролю (як <see cref="User.Password"/>).
        /// </remarks>
        /// <param name="nickname">Псевдонім користувача.</param>
        /// <returns>
        /// Якщо користувача з таким псевдонімом знайдено, конкретного користувача як <see cref="UserGetRequest"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
        /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
        /// </returns>
        [HttpGet]
        [Route("get/{nickname}")]
        public async Task<IActionResult> GetUser([FromRoute] string nickname)
        {
            User? user = await _dbContext.Users.FindAsync(nickname);

            if (user is null)
                return NotFound($"Користувача з псевдонімом '{nickname}' не знайдено.");

            return Ok((UserGetRequest)user);
        }

        #endregion

        // TODO: видалити методи запитів додавання.

        //#region Методи запитів додавання

        ///// <summary>
        ///// Дозволяє додати користувача у контекст бази даних.
        ///// </summary>
        ///// <remarks>
        ///// При додаванні користувача відбувається валідація моделі запиту додавання <paramref name="addUserRequest"/> за допомогою валідатора <see cref="UserIdentityValidator"/>.
        ///// </remarks>
        ///// <param name="addUserRequest">Модель користувача запиту додавання.</param>
        ///// <returns>
        ///// У випадку вдалого додавання користувача, модель запиту додавання користувача як <see cref="UserIdentity"/> (інтегрованого у відповідь <see cref="OkObjectResult"/>);<br>
        ///// інакше, у випадку невдалої валідації, список проблем валідації як <see cref="ValidationResult.Errors"/> (інтегрованого у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
        ///// </returns>
        //[HttpPost]
        //[Route("add")]
        //public async Task<IActionResult> AddUser(UserIdentity addUserRequest)
        //{
        //    UserIdentityValidator validator = new(_dbContext);
        //    ValidationResult validationResult = await validator.ValidateAsync(addUserRequest);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    else
        //    {
        //        User user = new()
        //        {
        //            Nickname = addUserRequest.Nickname,
        //            Password = addUserRequest.Password
        //        };

        //        _ = await _dbContext.Users.AddAsync(user);
        //        _ = await _dbContext.SaveChangesAsync();

        //        return Ok(user);
        //    }
        //}

        //#endregion

        #region Методи запитів перезапису

        /// <summary>
        /// Дозволяє оновити дані користувача за його псевдонімом. 
        /// </summary>
        /// <remarks>
        /// При оновленні даних користувача відбувається валідація моделі запиту оновлення <paramref name="updateUserRequest"/> за допомогою валідатора <see cref="UserUpdateRequestValidator"/>.
        /// </remarks>
        /// <param name="updateUserRequest">Модель користувача запиту оновлення.</param>
        /// <returns>
        /// 1. У випадку успішного оновлення даних, модель запиту оновлення користувача як <see cref="UserUpdateRequest"/> (інтегровану в HTTP-відповідь <see cref="OkObjectResult"/>).<br>
        /// 2. У випадку невдалого знаходження користувача, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br><br>
        /// 3. У випадку невдалої валідації, список помилок валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
        /// </returns>
        [HttpPut]
        [Route("update/{nickname}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string nickname, UserUpdateRequest updateUserRequest)
        {
            User? user = await _dbContext.Users.FindAsync(nickname);

            if (user is null)
                return NotFound($"Користувача з псевдонімом '{nickname}' не знайдено.");

            UserUpdateRequestValidator validator = new();
            ValidationResult validationResult = validator.Validate(updateUserRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            else
            {
                user.Password = updateUserRequest.Password;
                user.RightAnswers = updateUserRequest.RightAnswers;
                user.WrongAnswers = updateUserRequest.WrongAnswers;

                _ = await _dbContext.SaveChangesAsync();

                return Ok(user);
            }
        }

        #endregion
    }
}
