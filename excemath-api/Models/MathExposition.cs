namespace excemathApi.Models;

/// <summary>
/// Represents a simple math exposition for displaying that contains parts of normal text and LaTeX.
/// </summary>
public class MathExposition
{
    #nullable enable

    #region Properties

    /// <summary>
    /// A normal text part.
    /// </summary>
    public string? NormalText { get; set; }

    /// <summary>
    /// A LaTeX part.
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
