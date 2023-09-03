using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public record RecipeRequest
{
    public string title { get; set; }
    public string description { get; set; }
    public string instructions { get; set; }
    public double preparation_time { get; set; }
    public int servings { get; set; }
}
