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

using Microsoft.EntityFrameworkCore;

namespace excemathApi.Models;

/// <summary>
/// Представляє користувача, який має унікальний псевдонім, пароль, кількість правильних та неправильних відповідей.
/// </summary>
/// <remarks>
/// Має первинний ключ <see cref="Nickname"/>.
/// </remarks>
[PrimaryKey(nameof(Nickname))]
public class User
{
    /// <summary>
    /// Повертає або встановлює унікальний псевдонім поточного користувача.
    /// </summary>
    /// <remarks>
    /// Є первинним ключом.
    /// </remarks>
    public string Nickname { get; set; }

    /// <summary>
    /// Повертає або встановлює пароль поточного користувача.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Повертає або встановлює кількість правильних відповідей поточного користувача.
    /// </summary>
    /// <remarks>
    /// Має значення 0 за замовчуванням.
    /// </remarks>
    public int RightAnswers { get; set; } = 0;

    /// <summary>
    /// Повертає або встановлює кількість неправильних відповідей поточного користувача.
    /// </summary>
    /// <remarks>
    /// Має значення 0 за замовчуванням.
    /// </remarks>
    public int WrongAnswers { get; set; } = 0;
}
