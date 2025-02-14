using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public class BookAuthor
    {
        [Key, Required]
        public int author_id { get; set; }
        [Key, Required]
        public int book_id { get; set; }
        [Required]
        [JsonPropertyName("authorOrder")]
        public string author_order { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("royalityPercentage")]
        public double royality_percentage { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Author Author { get; set; } = null!;
    }
}
