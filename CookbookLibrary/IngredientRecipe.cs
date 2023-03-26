using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary
{
    [Table("IngredientRecipe")]
    public class IngredientRecipe
    {
        [ForeignKey("Ingredients")]
        public int ingredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        [ForeignKey("Recipes")]
        public int recipeId { get; set; }
        public Recipe Recipe { get; set; }

        public float quantity { get; set; }
        public string unit { get; set; }
    }
}
