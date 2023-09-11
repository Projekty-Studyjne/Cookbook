using CookbookLibrary.Entities;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public record IngredientResponse( int Id, string name, string category, float? quantity, string? unit );
