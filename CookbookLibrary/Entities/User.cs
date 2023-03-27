using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]{5,19}$", ErrorMessage = "Characters are not allowed")]
        public string username { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Characters are not allowed")]
        public string email { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d!@#$%^&*()]{8,20}$", ErrorMessage = "Characters are not allowed")]
        public string password { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<UserRecipe> UserRecipes { get; set; }
    }
}
