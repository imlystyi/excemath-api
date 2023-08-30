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

using Microsoft.AspNetCore.Mvc;

namespace excemathApi.Controllers;

/// <summary>
/// Represents a factory for custom <see cref="ActionResult"/> objects.
/// </summary>
public static class ControllerResults
{
    #nullable enable

    #region Methods

    /// <summary>
    /// Creates a new instance of the <see cref="InternalServerErrorObjectResult"/> class with the specified content.
    /// </summary>
    /// <param name="content">The content to format into the result body.</param>
    /// <returns>An <see cref="InternalServerErrorObjectResult"/> object.</returns>
    public static InternalServerErrorObjectResult InternalServerError(object? content = null) => new(content);

    #endregion

    #region Nested classes

    /// <summary>
    /// An <see cref="ObjectResult"/> that represents standard HTTP <c>500 Internal Server Error</c> with the specified content.
    /// </summary>
    public sealed class InternalServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// The status code of the result.
        /// </summary>
        public const int DEFAULT_STATUS_CODE = 500;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorObjectResult"/> class.
        /// </summary>
        /// <param name="content">The content to format into the result body.</param>
        public InternalServerErrorObjectResult(object? content) : base(content) =>
            this.StatusCode = DEFAULT_STATUS_CODE;
    }

    #endregion
}
