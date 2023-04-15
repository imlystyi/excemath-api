using FluentValidation;
using excemathApi.Data;
using excemathApi.Models;

namespace excemathApi.Validators
{
    /// <summary>
    /// Представляє валідатор для класу моделі <see cref="AddUserRequest"/>.
    /// </summary>
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        #region Поля

        // Контекст бази даних.
        private readonly UsersApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="AddUserRequestValidator"/>.
        /// </summary>
        /// <remarks>
        /// Валідує такі дані: <br>
        ///  1. <see cref="AddUserRequest.Nickname"/>: на те, чи є пустим або <see langword="null"/>-рядком; на те, чи є користувач із таким псевдонімом у контексті бази даних <paramref name="dbContext"/>.</br><br>
        ///  2. <see cref="AddUserRequest.Password"/>: на те, чи є пустим або <see langword="null"/>-рядком.</br>
        /// </remarks>
        /// <param name="dbContext">Контекст бази даних.</param>
        public AddUserRequestValidator(UsersApiDbContext dbContext)
        {
            _dbContext = dbContext;

            _ = RuleFor(user => user.Nickname)
                .NotEmpty().WithErrorCode("01").WithMessage("Неправильний псевдонім.")
                .NotNull().WithErrorCode("01").WithMessage("Неправильний псевдонім.")
                .MustAsync(async (nickname, cancellation) =>
                {
                    var exists = await _dbContext.Users.FindAsync(new object[] { nickname }, cancellation);
                    return !(exists is not null);
                }).WithErrorCode("03").WithMessage("Користувач з таким псевдонімом вже існує.");

            RuleFor(user => user.Password)
                .NotEmpty().WithErrorCode("02").WithMessage("Неправильний пароль.")
                .NotNull().WithErrorCode("02").WithMessage("Неправильний пароль.");
        }

        #endregion
    }
}
