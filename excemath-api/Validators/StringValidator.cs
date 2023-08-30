// excemath API - open source API for educational projects related to mathematics
// Copyright (C) 2023  miu-miu enjoyers
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Contact us:
// i.   By paper mail: 23 Yevhena Patona street, Zaliznychnyi raion, Lviv, Lviv oblast, 79040, Ukraine
// ii.  By email: vladyslav.yakubovskyi.work@gmail.com
//
// See the official repository page on GitHub: <https://github.com/miu-miu-enjoyers/excemath-api>

using excemathApi.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace excemathApi.Validators;

/// <summary>
/// Represents a validator to the <see cref="Student"/> class object properties, that represented by a <see cref="string"/>.
/// </summary>
/// <remarks>Validator rules are described in the file "StringValidator Validation Rules.txt".</remarks>
public partial class StringValidator : AbstractValidator<string>
{
    #region Constructors

    #nullable enable

    /// <summary>
    /// Initializes a new instance of the <see cref="StringValidator"/> class.
    /// </summary>
    /// <param name="propertyName">The name of the property to be validated.</param>
    public StringValidator(string propertyName)
    {
        #nullable restore

        _ = propertyName switch
        {
            nameof(StudentDto.Nickname) => RuleFor(vv => vv)
                .NotEmpty().NotNull().WithErrorCode("N1")
                .Must(vv => IsLatinAndDigitsOnly().IsMatch(vv)).WithErrorCode("N2"),
            nameof(StudentDto.FirstName) or nameof(StudentDto.LastName) or nameof(StudentDto.Location) => RuleFor(vv => vv)
                .NotEmpty().NotNull().WithErrorCode("N1")
                .Must(vv => IsOwnName().IsMatch(vv)).WithErrorCode("N3"),
            nameof(StudentDto.About) => RuleFor(vv => vv)
                .NotEmpty().NotNull().WithErrorCode("S1"),
            _ => throw new ArgumentException("Unknown value kind to be validated."),
        };
    }

    #endregion

    #region Regular expressions

    [GeneratedRegex("^[a-zA-Z0-9_]+$")]
    private static partial Regex IsLatinAndDigitsOnly();

    [GeneratedRegex("^[a-zA-Z0-9_А-Яа-я()\\.?!']+$")]
    private static partial Regex IsOwnName();

    #endregion
}
