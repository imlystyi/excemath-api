#nullable enable

using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using excemathApi.Data;
using excemathApi.Models;
using excemathApi.Validators;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="UsersApiDbContext"/>, який дозволяє <b>додавати, оновлювати та обробляти</b> дані, а саме виконувати реєстрацію, зміну даних користувача та його авторизацію.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersAuthenticationController : Controller
{
    #region Поля

    // Набір конфігурацій API.
    private readonly IConfiguration _configuration;

    // Контекст бази даних контролера.
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

    /// <summary>
    /// Дозволяє клієнту отримати успішність авторизації користувача, визначивши її за вказаною моделлю ідентичності.
    /// </summary>
    /// <param name="userIdentity">Модель ідентичності користувача.</param>
    /// <returns>Якщо авторизація пройшла успішно, <see langword="true"/> (інтегроване у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, <see langword="false"/> (інтегроване у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br></returns>
    [HttpPost]
    [Route("authorize")]
    public async Task<IActionResult> Authorize([FromQuery] UserIdentity userIdentity)
    {
        User? user = await _dbContext.Users.FindAsync(userIdentity.Nickname);

        if (user is not null && DecryptPassword(user.Password) == userIdentity.Password)
            return Ok();

        else
            return BadRequest();
    }

    /// <summary>
    /// Дозволяє клієнту зареєструвати користувача за вказаною моделлю ідентичності (додати його в фізичну базу даних).
    /// </summary>
    /// <param name="userIdentity">Модель ідентичності користувача.</param>
    /// <returns>
    /// У випадку вдалої реєстрації користувача, модель користувача як <see cref="User"/> (інтегрована у відповідь <see cref="OkObjectResult"/>);<br>
    /// інакше, у випадку невдалої валідації, список проблем валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
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

    /// <summary>
    /// Дозволяє оновити дані користувача за його псевдонімом. 
    /// </summary>
    /// <remarks>
    /// При оновленні даних користувача відбувається валідація моделі запиту оновлення <paramref name="updateUserRequest"/> за допомогою валідатора <see cref="UserUpdateRequestValidator"/>.
    /// </remarks>
    /// <param name="userUpdateRequest">Модель користувача запиту оновлення.</param>
    /// <returns>
    /// 1. У випадку успішного оновлення даних, модель запиту оновлення користувача як <see cref="UserUpdateRequest"/> (інтегровану в HTTP-відповідь <see cref="OkObjectResult"/>).<br>
    /// 2. У випадку невдалого знаходження користувача, HTTP-відповідь <see cref="NotFoundObjectResult"/>.</br><br>
    /// 3. У випадку невдалої валідації, список помилок валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
    /// </returns>
    [HttpPut]
    [Route("update/{nickname}")]
    public async Task<IActionResult> Update([FromRoute] string nickname, UserUpdateRequest userUpdateRequest)
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
            user.Password = userUpdateRequest.Password;
            user.RightAnswers = userUpdateRequest.RightAnswers;
            user.WrongAnswers = userUpdateRequest.WrongAnswers;

            _ = await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

    // Ці методи шифрування використовують стандарт шифрування AES і виконуються за допомогою ключа,
    // вказаного у appsetings.json (у "CodingKeys:Password").

    // Зашифровує пароль.
    private string EncryptPassword(string password)
    {
        byte[] key = Encoding.UTF8.GetBytes(_configuration["CodingKeys:Password"]!),
            decipheredArray;

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

    // Розшифровує пароль.
    private string DecryptPassword(string password)
    {
        byte[] key = Encoding.UTF8.GetBytes(_configuration["CodingKeys:Password"]!),
            chipheredArray = Encoding.UTF8.GetBytes(password);

        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = new byte[16];

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream ms = new(chipheredArray);
        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    #endregion
}
