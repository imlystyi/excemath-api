using excemathApi.Data;
using excemathApi.Models;
using FluentValidation;

namespace excemathApi.Validators;

/// <summary>
/// Представляє валідатор для об'єктів класу <see cref="UserIdentity"/>.
/// </summary>
public class UserIdentityValidator : AbstractValidator<UserIdentity>
{
    #region Поля

    private readonly UsersApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UserIdentityValidator"/> із зазначеним контекстом бази даних.
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    public UserIdentityValidator(UsersApiDbContext dbContext)
    {
        _dbContext = dbContext;

        _ = RuleFor(user => user.Nickname)
            .NotEmpty().WithMessage("Неправильний нікнейм!").WithErrorCode("01")
            .NotNull().WithMessage("Неправильний нікнейм!").WithErrorCode("01")
            .MustAsync(async (nickname, cancellation) =>
            {
                var exists = await _dbContext.Users.FindAsync(new object[] { nickname }, cancellation);
                return !(exists is not null);
            }).WithMessage("Користувач з таким нікнеймом вже існує!").WithErrorCode("02");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Неправильний пароль!").WithErrorCode("03")
            .NotNull().WithMessage("Неправильний пароль!").WithErrorCode("03");
    }

    #endregion
}
