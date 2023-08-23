using excemathApi.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace excemathApi.Validators;

// TODO: StringValidator class documentation.

/// <summary>
/// 
/// </summary>
public partial class StringValidator : AbstractValidator<string>
{
#nullable enable

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    public StringValidator(string? propertyName)
    {
        if (propertyName is null)   // If propertyName is null, checking string as password.
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

#nullable restore

    #region Regular expressions

    [GeneratedRegex("^[a-zA-Z0-9_]+$")]
    private static partial Regex IsLatinAndDigitsOnly();

    [GeneratedRegex("^[a-zA-Z0-9_А-Яа-я()\\.?!']+$")]
    private static partial Regex IsOwnName();

    #endregion
}