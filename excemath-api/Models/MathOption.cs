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
/// Represents an option in the answer options list of the math problem.
/// </summary>
public class MathOption
{
    #region Properties

    /// <summary>
    /// Gets or sets whether to render the answer option as LaTeX.
    /// </summary>
    public bool RenderAsLatex { get; set; }

    /// <summary>
    /// Gets or sets the index of the answer option in the answer options list.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the content of the option.
    /// </summary>
    public string Content { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MathOption"/> class.
    /// </summary>
    public MathOption() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathOption"/> class with the specified parameters.
    /// </summary>
    /// <param name="renderAsLatex">Defines whether to render the answer option as LaTeX.</param>
    /// <param name="index">The index of the answer option in the answer options list.</param>
    /// <param name="content">The content of the option.</param>
    public MathOption(bool renderAsLatex, int index, string content)
    {
        this.RenderAsLatex = renderAsLatex;
        this.Index = index;
        this.Content = content;
    }

    #endregion
}
