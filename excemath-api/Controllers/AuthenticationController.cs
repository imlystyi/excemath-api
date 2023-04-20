#nullable enable

using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using excemathApi.Data;
using excemathApi.Models;
using excemathApi.Validators;

namespace excemathApi.Controllers
{
    /// <summary>
    /// Представляє контролер для автентифікації користувача як моделі <see cref="User"/>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        #region Поля

        // Набір конфігурацій API.
        private readonly IConfiguration _configuration;

        // Контекст бази даних контролера.
        private readonly UsersApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="AuthenticationController"/>, використовуючи вказаний контекст бази даних та набір конфігурацій.
        /// </summary>
        /// <param name="dbContext">Контекст бази даних.</param>
        /// <param name="configuration">Набір конфігурацій.</param>
        public AuthenticationController(UsersApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        #endregion

        #region Методи запитів отримання

        /// <summary>
        /// Дозволяє отримати успішність автентифікації користувача, визначивши її за вказаною моделлю ідентичності.
        /// </summary>
        /// <param name="userIdentity">Модель ідентичності користувача.</param>
        /// <returns>Якщо автентифікація пройшла успішно, <see langword="true"/> (інтегроване у HTTP-відповідь <see cref="OkObjectResult"/>);<br>
        /// інакше, <see langword="false"/> (інтегроване у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br></returns>
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserIdentity userIdentity)
        {
            User? user = await _dbContext.Users.FindAsync(userIdentity.Nickname);

            if (user is not null && user.Password == EncryptPassword(userIdentity.Password))
                return Ok(true);

            else
                return BadRequest(false);
        }

        #endregion

        #region Методи запитів додавання

        /// <summary>
        /// Дозволяє зареєструвати користувача за вказаною моделлю ідентичності (додати його в фізичну базу даних).
        /// </summary>
        /// <param name="userIdentity">Модель ідентичності користувача.</param>
        /// <returns>
        /// У випадку вдалої реєстрації користувача, модель користувача як <see cref="User"/> (інтегрована у відповідь <see cref="OkObjectResult"/>);<br>
        /// інакше, у випадку невдалої валідації, список проблем валідації як <see cref="ValidationResult.Errors"/> (інтегрований у HTTP-відповідь <see cref="BadRequestObjectResult"/>).</br>
        /// </returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserIdentity userIdentity)
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

                return Ok(user);
            }
        }

        #endregion

        #region Методи шифрування

        // TODO: зробити DecryptPassword.

        // Шифрує пароль за допомогою ключа, вказаного у appsettings.json (у "CodingKeys:Password").
        private string EncryptPassword(string password)
        {
            byte[] key = Encoding.UTF8.GetBytes(_configuration["CodingKeys:Password"]!),
                   array;

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = new byte[16];

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new())
            {
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);

                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(password);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        #endregion
    }
}
