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

using excemathApi.Models;
using FluentValidation;

namespace excemathApi.Validators;

/// <summary>
/// Представляє валідатор для об'єктів класу <see cref="UserUpdateRequest"/>.
/// </summary>
public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    /// <summary>
    /// Створює екземпляр класу <see cref="UserUpdateRequestValidator"/>.
    /// </summary>
    public UserUpdateRequestValidator() => _ = RuleFor(user => user.Password)
        .NotEmpty().WithMessage("Неправильний пароль.").WithErrorCode("03")
        .NotNull().WithMessage("Неправильний пароль.").WithErrorCode("03");
}
