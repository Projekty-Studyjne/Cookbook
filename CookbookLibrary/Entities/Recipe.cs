using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Entities
{
    [Table("Recipes")]
    public class Recipe
    {
        [Key]
        public int recipeId { get; set; }
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [StringLength(200)]
        public string description { get; set; }
        [StringLength(1000)]
        public string instructions { get; set; }
        [Range(0.1, double.MaxValue)]
        public double preparation_time { get; set; }
        [Range(1, int.MaxValue)]
        public int servings { get; set; }

        public virtual ICollection<IngredientRecipe> IngredientRecipes { get; set; }

        public virtual ICollection<UserRecipe> UserRecipes { get; set; }

        public virtual ICollection<CategoryRecipe> CategoryRecipes { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

    }
}
