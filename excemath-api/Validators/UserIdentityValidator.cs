using FluentValidation;
using excemathApi.Data;
using excemathApi.Models;

namespace excemathApi.Validators;

/// <summary>
/// Представляє валідатор для класу моделі <see cref="UserIdentity"/>.
/// </summary>
public class UserIdentityValidator : AbstractValidator<UserIdentity>
{
    #region Поля

    // Контекст бази даних.
    private readonly UsersApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UserIdentityValidator"/>.
    /// </summary>
    /// <remarks>
    /// Валідує такі дані: <br>
    ///  1. <see cref="UserIdentity.Nickname"/>: на те, чи є пустим або <see langword="null"/>-рядком; на те, чи є користувач із таким псевдонімом у контексті бази даних <paramref name="dbContext"/>.</br><br>
    ///  2. <see cref="UserIdentity.Password"/>: на те, чи є пустим або <see langword="null"/>-рядком.</br>
    /// </remarks>
    /// <param name="dbContext">Контекст бази даних.</param>
    public UserIdentityValidator(UsersApiDbContext dbContext)
    {
        _dbContext = dbContext;

        _ = RuleFor(user => user.Nickname)
            .NotEmpty().WithMessage("Неправильний псевдонім.").WithErrorCode("01")
            .NotNull().WithMessage("Неправильний псевдонім.").WithErrorCode("01")
            .MustAsync(async (nickname, cancellation) =>
            {
                var exists = await _dbContext.Users.FindAsync(new object[] { nickname }, cancellation);
                return !(exists is not null);
            }).WithMessage("Користувач з таким псевдонімом вже існує.").WithErrorCode("02");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Неправильний пароль.").WithErrorCode("03")
            .NotNull().WithMessage("Неправильний пароль.").WithErrorCode("03");
    }

    #endregion
}
