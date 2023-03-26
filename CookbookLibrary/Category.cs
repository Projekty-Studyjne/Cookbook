using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int categoryId { get; set; }
        [Required]
        [StringLength(30)]
        public string name { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public virtual ICollection<CategoryRecipe> CategoryRecipes { get; set; }
    }
}
