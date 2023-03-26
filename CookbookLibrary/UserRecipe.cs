using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary
{
    [Table("UserRecipe")]
    public class UserRecipe
    {
        [ForeignKey("Users")]
        public int userId { get; set; }
        public User User { get; set; }
        [ForeignKey("Recipes")]
        public int recipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
