namespace excemathApi.Models;

/// <summary>
/// 
/// </summary>
public class Exposition
{
#nullable enable

    /// <summary>
    /// 
    /// </summary>
    public string? NormalText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Latex { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Exposition() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="normalText"></param>
    /// <param name="latex"></param>
    public Exposition(string? normalText, string? latex)
    {
        this.NormalText = normalText;
        this.Latex = latex;
    }
}
