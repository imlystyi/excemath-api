namespace excemathApi.Models;

/// <summary>
/// 
/// </summary>
public class Option
{
    /// <summary>
    /// 
    /// </summary>
    public bool RenderAsLatex { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Value { get; set; }

    public Option(bool renderAsLatex, int number, string value) => (RenderAsLatex, Number, Value) = (renderAsLatex, number, value);


}
