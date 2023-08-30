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

namespace excemathApi.Models;

/// <summary>
/// Represents a simple math exposition for displaying that contains parts of normal text and LaTeX.
/// </summary>
public class MathExposition
{
    #nullable enable

    #region Properties

    /// <summary>
    /// Gets or sets a normal text part.
    /// </summary>
    public string? NormalText { get; set; }

    /// <summary>
    /// Gets or sets a LaTeX part.
    /// </summary>
    public string? Latex { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MathExposition"/> class.
    /// </summary>
    public MathExposition() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathExposition"/> class with the specified parameters.
    /// </summary>
    /// <param name="normalText">A normal text part.</param>
    /// <param name="latex">A LaTeX part.</param>
    public MathExposition(string? normalText, string? latex)
    {
        this.NormalText = normalText;
        this.Latex = latex;
    }

    #endregion
}
