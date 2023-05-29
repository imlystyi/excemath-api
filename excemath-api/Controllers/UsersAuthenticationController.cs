using excemathApi.Data;
using excemathApi.Models;
using excemathApi.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="UsersApiDbContext"/>, який дозволяє <b>додавати, оновлювати та обробляти</b> дані, а саме виконувати авторизацію, реєстрацію та зміну даних користувача.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersAuthenticationController : Controller
{
    #region Поля

    private readonly IConfiguration _configuration;

    private readonly UsersApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UsersAuthenticationController"/>, використовуючи вказаний контекст бази даних та набір конфігурацій.
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    /// <param name="configuration">Набір конфігурацій.</param>
    public UsersAuthenticationController(UsersApiDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    #endregion

    #region Методи

#nullable enable

    /// <summary>
    /// Дозволяє клієнту отримати інформацію про успішність авторизації користувача, визначивши її за вказаною ідентичністю користувача (об'єктом класу <see cref="UserIdentity"/>).
    /// </summary>
    /// <param name="userIdentity">Ідентичність користувача.</param>
    /// <returns>
    /// У випадку успішної авторизації, HTTP-відповідь <see cref="OkObjectResult"/>; інакше, HTTP-відповідь <see cref="BadRequestObjectResult"/>.
    /// </returns>
    [HttpPost]
    [Route("authorize")]
    public async Task<IActionResult> Authorize([FromQuery] UserIdentity userIdentity)
    {
        User? user = await _dbContext.Users.FindAsync(userIdentity.Nickname);

        if (user is not null && user.Password == EncryptPassword(userIdentity.Password))
            return Ok();

        else
            return BadRequest();
    }

    /// <summary>
    /// Дозволяє клієнту оновити дані користувача за його псевдонімом, використовуючи вказаного користувача для запиту оновлення (об'єкт класу <see cref="UserUpdateRequest"/>).
    /// </summary>
    /// <remarks>
    /// При оновленні даних користувача відбувається валідація моделі запиту оновлення <paramref name="updateUserRequest"/> за допомогою валідатора <see cref="UserUpdateRequestValidator"/>.
    /// </remarks>
    /// <param name="nickname">Псевдонім користувача.</param>
    /// <param name="userUpdateRequest">Користувач для запиту оновлення.</param>
    /// <returns>
    /// У випадку успішного оновлення даних, HTTP-відповідь <see cref="OkObjectResult"/>; інакше, якщо користувача не було успішно знайдено, HTTP-відповідь <see cref="NotFoundObjectResult"/>; інакше, у випадку невдалої валідації, список помилок валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).
    /// </returns>
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromQuery] string nickname, [FromQuery] UserUpdateRequest userUpdateRequest)
    {
        User? user = await _dbContext.Users.FindAsync(nickname);

        if (user is null)
            return NotFound();

        UserUpdateRequestValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(userUpdateRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        else
        {
            user.Password = EncryptPassword(userUpdateRequest.Password);
            user.RightAnswers = userUpdateRequest.RightAnswers;
            user.WrongAnswers = userUpdateRequest.WrongAnswers;

            _ = await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

#nullable restore

    /// <summary>
    /// Дозволяє клієнту зареєструвати користувача, використовуючи вказану ідентичність користувача (об'єкт класу <see cref="UserIdentity"/>).
    /// </summary>
    /// <param name="userIdentity">Ідентичність користувача.</param>
    /// <returns>
    /// У випадку успішної реєстрації, HTTP-відповідь <see cref="OkObjectResult"/>; інакше, у випадку невдалої валідації, список проблем валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).
    /// </returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromQuery] UserIdentity userIdentity)
    {
        UserIdentityValidator validator = new(_dbContext);
        ValidationResult validationResult = await validator.ValidateAsync(userIdentity);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        else
        {
            User user = new()
            {
                Nickname = userIdentity.Nickname,
                Password = EncryptPassword(userIdentity.Password)
            };

            _ = await _dbContext.Users.AddAsync(user);
            _ = await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

    // Ці методи шифрування паролю використовують стандарт шифрування AES і виконуються за допомогою ключа, вказаного у appsetings.json (у "CodingKeys:Password").

    private string EncryptPassword(string password)
    {
        byte[] key = Encoding.UTF8.GetBytes(_configuration["CodingKeys:Password"]!);
        byte[] decipheredArray;

        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = new byte[16];

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using (MemoryStream ms = new())
        {
            using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);

            using (StreamWriter sw = new(cs))
            {
                sw.Write(password);
            }

            decipheredArray = ms.ToArray();
        }

        return Convert.ToBase64String(decipheredArray);
    }

    #endregion
}
