using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int commentId { get; set; }
        [StringLength(300)]
        public string comment { get; set; }
        public int ratingId { get; set; }
        [ForeignKey("ratingId")]
        public virtual Rating Rating { get; set; }
    }
}
