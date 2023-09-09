using CookbookLibrary.Entities;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public record RecipeResponse(int Id, string title,string imageUrl, string description, string instructions, double preparation_time, int servings);
