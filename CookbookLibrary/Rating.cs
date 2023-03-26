using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        public int ratingId { get; set; }
        [Range(1,5)]
        public int rating { get; set; }
        [ForeignKey("Users")]
        public int userId { get; set; }
        [ForeignKey("Recipes")]
        public int recipeId { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual User User { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
