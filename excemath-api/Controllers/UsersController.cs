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

      // Контекст бази даних контролеру.
      private readonly UsersApiDbContext _dbContext;

      #endregion

      #region Конструктори

      /// <summary>
      /// Створює екземпляр класу <see cref="UsersController"/>, використовуючи зазначений контекст бази даних. 
      /// </summary>
      /// <param name="dbContext">Контекст бази даних.</param>
      public UsersController(UsersApiDbContext dbContext) => _dbContext = dbContext;

      #endregion

      #region Методи запитів отримання

      /// <summary>
      /// Дозволяє отримати всіх користувачів у вигляді списку.
      /// </summary>
      /// <remarks>
      /// Користувачі у списку повертаються як моделі запиту отримання <see cref="GetUserRequest"/>, які не мають властивості паролю (як <see cref="User.Password"/>).
      /// </remarks>
      /// <returns>
      /// Список користувачів як список <see cref="List{GetUserRequest}"/> з елементів класу <see cref="GetUserRequest"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
      /// </returns>
      [HttpGet]
      [Route("get")]
      public async Task<IActionResult> GetAllUsers()
      {

         return Ok((await _dbContext.Users.ToListAsync()).ConvertAll(u => (GetUserRequest)u));
      }

      /// <summary>
      /// Дозволяє отримати конкретного користувача за його псевдонімом.
      /// </summary>
      /// <remarks>
      /// Користувач повертається як модель запиту отримання <see cref="GetUserRequest"/>, яка не має властивості паролю (як <see cref="User.Password"/>).
      /// </remarks>
      /// <param name="nickname">Псевдонім користувача.</param>
      /// <returns>
      /// Якщо користувача знайдено, конкретного користувача як <see cref="GetUserRequest"/> (інтегрованого у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
      /// інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br>
      /// </returns>
      [HttpGet]
      [Route("get/{nickname}")]
      public async Task<IActionResult> GetUser([FromRoute] string nickname)
      {
         User? user = await _dbContext.Users.FindAsync(nickname);

         if (user is null)
            return NotFound($"Користувача з псевдонімом '{nickname}' не знайдено.");

         return Ok((GetUserRequest)user);
      }

      /*      [HttpGet]
            [Route("rating/{nickname}")]
            public async Task<IActionResult> GetRatingOfUser([FromRoute] string nickname)
            {
               User? user = await _dbContext.Users.FindAsync(nickname);
               if (user is null)
                  return NotFound($"Користувача з псевдонімом '{nickname}' не знайдено.");
               var rightAnswers = user.RightAnswers;
               var wrongAnswers = user.WrongAnswers;
                var totalAnswers = rightAnswers + wrongAnswers;

               if (totalAnswers == 0)
               {
                  return Ok(0);
               }

               var rating = (double)rightAnswers / wrongAnswers;

               return Ok(rating);
            }*/

      [HttpGet]
      [Route("rating")]
      public async Task<IActionResult> GetRatingList()
      {
         var users = await _dbContext.Users.ToListAsync();
         var ratings = users.Select(user =>
         {
            var rightAnswers = user.RightAnswers;
            var wrongAnswers = user.WrongAnswers;
            var totalAnswers = rightAnswers + wrongAnswers;
            var rating = totalAnswers > 0 ? (double)rightAnswers / wrongAnswers : 0;
            return new { Nickname = user.Nickname, Rating = rating };
         }).OrderByDescending(r => r.Rating).ToList();

         return Ok(ratings);
      }

      #endregion

      #region Методи запитів додавання

      /// <summary>
      /// Дозволяє додати користувача у контекст бази даних.
      /// </summary>
      /// <remarks>
      /// При додаванні користувача відбувається валідація моделі запиту додавання <paramref name="addUserRequest"/> за допомогою валідатора <see cref="AddUserRequestValidator"/>.
      /// </remarks>
      /// <param name="addUserRequest">Модель користувача запиту додавання.</param>
      /// <returns>
      /// У випадку вдалого додавання користувача, модель запиту додавання користувача як <see cref="AddUserRequest"/> (інтегрованого у відповідь <see cref="OkObjectResult"/>);<br>
      /// інакше, у випадку невдалої валідації, список проблем валідації як <see cref="ValidationResult.Errors"/> (інтегрованого у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
      /// </returns>
      [HttpPost]
      [Route("add")]
      public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
      {
         AddUserRequestValidator validator = new(_dbContext);
         ValidationResult validationResult = await validator.ValidateAsync(addUserRequest);

         if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

         else
         {
            User user = new()
            {
               Nickname = addUserRequest.Nickname,
               Password = addUserRequest.Password
            };

            _ = await _dbContext.Users.AddAsync(user);
            _ = await _dbContext.SaveChangesAsync();

            return Ok(user);
         }
      }

      #endregion

      #region Методи запитів умовного оновлення 

      /// <summary>
      /// Дозволяє оновити дані користувача за його псевдонімом. 
      /// </summary>
      /// <remarks>
      /// При оновленні даних користувача відбувається валідація моделі запиту оновлення <paramref name="updateUserRequest"/> за допомогою валідатора <see cref="UpdateUserRequestValidator"/>.
      /// </remarks>
      /// <param name="updateUserRequest">Модель користувача запиту оновлення.</param>
      /// <returns>
      /// 1. У випадку успішного оновлення даних, модель запиту оновлення користувача як <see cref="UpdateUserRequest"/> (інтегровану в HTTP-відповідь <see cref="OkObjectResult"/>).<br>
      /// 2. У випадку невдалого знаходження користувача, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br><br>
      /// 3. У випадку невдалої валідації, список помилок валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
      /// </returns>
      [HttpPut]
      [Route("update/{nickname}")]
      public async Task<IActionResult> UpdateUser([FromRoute] string nickname, UpdateUserRequest updateUserRequest)
      {
         User? user = await _dbContext.Users.FindAsync(nickname);

         if (user is null)
            return NotFound($"Користувача з псевдонімом '{nickname}' не знайдено.");

         UpdateUserRequestValidator validator = new();
         ValidationResult validationResult = validator.Validate(updateUserRequest);

         if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

         else
         {
            user.Password = updateUserRequest.Password;
            user.RightAnswers = updateUserRequest.RightAnswers;

            _ = await _dbContext.SaveChangesAsync();

            return Ok(user);
         }
      }

      #endregion

      // TODO: подумати, чи варто робити метод запиту вилучення.
      //#region Методи запитів вилучення

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="id"></param>
      ///// <returns></returns>
      //[HttpDelete]
      //[Route("delete/{id:guid}")]
      //public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
      //{
      //    User? user = await _dbContext.Users.FindAsync(id);

      //    if (user is null)
      //        return NotFound();

      //    _ = _dbContext.Users.Remove(user);

      //    _ = await _dbContext.SaveChangesAsync();

      //    return Ok(user);
      //}

      //#endregion
   }
}
