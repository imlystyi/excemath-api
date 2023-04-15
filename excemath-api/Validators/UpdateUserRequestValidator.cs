using FluentValidation;
using excemathApi.Models;

namespace excemathApi.Validators
{
    /// <summary>
    /// Представляє валідатор для класу моделі <see cref="UpdateUserRequest"/>.
    /// </summary>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        #region Конструктори

        /// <summary>
        /// Створює екземпляр класу <see cref="UpdateUserRequestValidator"/>.
        /// </summary>
        /// <remarks>
        /// Валідує такі дані:<br>
        /// <see cref="UpdateUserRequest.Password"/>: на те, чи є пустим або <see langword="null"/>-рядком.</br>
        /// </remarks>
        public UpdateUserRequestValidator() => RuleFor(user => user.Password)
            .NotEmpty().WithErrorCode("02").WithMessage("Неправильний пароль.")
            .NotNull().WithErrorCode("02").WithMessage("Неправильний пароль.");

        #endregion
    }
}
