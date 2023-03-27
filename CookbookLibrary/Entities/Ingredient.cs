using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Entities
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        public int ingredientId { get; set; }
        [StringLength(30)]
        public string name { get; set; }
        [StringLength(50)]
        public string category { get; set; }
        public virtual ICollection<IngredientRecipe> IngredientRecipes { get; set; }

    }
}
