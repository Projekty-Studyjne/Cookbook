using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public record RatingRequest
{
    public int rating { get; set; }
    public int userId { get; set; }
    public int recipeId {get; set; }
}
