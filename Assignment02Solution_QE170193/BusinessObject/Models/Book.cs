using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int book_id { get; set; }
        [Required]
        public string title { get; set; } = string.Empty;
        [Required]
        public string type { get; set; } = string.Empty;
        [Required]
        public int pub_id { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string advance { get; set; } = string.Empty;
        [Required]
        public double royalty { get; set; }
        [Required]
        public int ytd_sales { get; set; }
        [Required]
        public string notes { get; set; } = string.Empty;
        [Required]
        public DateTime published_date { get; set; }


        public virtual Publisher Publisher { get; set; } = null!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = [];
    }
}
