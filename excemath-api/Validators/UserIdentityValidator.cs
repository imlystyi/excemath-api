/* excemath - an app for preparing for math exams.
* Copyright (C) 2023 miu-miu enjoyers

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program. If not, see <https://www.gnu.org/licenses/>. */

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
