using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary
{
    [Table("CategoryRecipe")]
    public class CategoryRecipe
    {
        [ForeignKey("Categories")]
        public int categoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Recipes")]
        public int recipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
