using excemathApi.Models;
using FluentValidation;

namespace excemathApi.Validators;

/// <summary>
/// Представляє валідатор для об'єктів класу <see cref="UserUpdateRequest"/>.
/// </summary>
public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UserUpdateRequestValidator"/>.
    /// </summary>
    public UserUpdateRequestValidator() => _ = RuleFor(user => user.Password)
        .NotEmpty().WithMessage("Неправильний пароль.").WithErrorCode("03")
        .NotNull().WithMessage("Неправильний пароль.").WithErrorCode("03");

    #endregion
}
