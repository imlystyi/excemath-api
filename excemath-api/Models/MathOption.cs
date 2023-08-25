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
