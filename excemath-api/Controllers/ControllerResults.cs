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
