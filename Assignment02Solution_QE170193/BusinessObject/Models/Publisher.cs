using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Publisher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pub_id { get; set; }
        [Required]
        public string publisher_name { get; set; } = string.Empty;
        [Required]
        public string city { get; set; } = string.Empty;
        [Required]
        public string state { get; set; } = string.Empty;
        [Required]
        public string country { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = [];
        public virtual ICollection<Book> Books { get; set; } = [];
    }
}
