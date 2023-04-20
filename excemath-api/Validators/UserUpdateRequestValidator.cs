using FluentValidation;
using excemathApi.Models;

namespace excemathApi.Validators
{
    /// <summary>
    /// Представляє валідатор для класу моделі <see cref="UserUpdateRequest"/>.
    /// </summary>
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="UserUpdateRequestValidator"/>.
        /// </summary>
        /// <remarks>
        /// Валідує такі дані:<br>
        /// <see cref="UserUpdateRequest.Password"/>: на те, чи є пустим або <see langword="null"/>-рядком.</br>
        /// </remarks>
        public UserUpdateRequestValidator() => RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Неправильний пароль.")
            .NotNull().WithMessage("Неправильний пароль.");

        #endregion
    }
}
