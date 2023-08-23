using excemathApi.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace excemathApi.Validators;

/// <summary>
/// Represents a validator to the <see cref="Student"/> class object properties, that represented by a <see cref="string"/>.
/// </summary>
/// <remarks>Validator rules are described in the file StringValidatorRules.txt</remarks>
public partial class StringValidator : AbstractValidator<string>
{
    #region Constructors

    #nullable enable

    /// <summary>
    /// Initializes a new instance of the <see cref="StringValidator"/> class.
    /// </summary>
    /// <param name="propertyName">The name of the property to be validated; if nothing or null is entered, the string is validated as a password.</param>
    public StringValidator(string? propertyName = null)
    {
    #nullable restore

        if (propertyName is null)   // If propertyName is null, validating string as a password.
        {
            _ = RuleFor(vv => vv)
                .NotEmpty().NotNull().WithErrorCode("P1");
        }
        else
        {
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
    }

    #endregion

    #region Regular expressions

    [GeneratedRegex("^[a-zA-Z0-9_]+$")]
    private static partial Regex IsLatinAndDigitsOnly();

    [GeneratedRegex("^[a-zA-Z0-9_А-Яа-я()\\.?!']+$")]
    private static partial Regex IsOwnName();

    #endregion
}