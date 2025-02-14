using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public class Author
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int author_id { get; set; }
        [Required]
        [JsonPropertyName("lastName")]
        public string last_name { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("firstName")]
        public string first_name { get; set; } = string.Empty;
        [Required]
        public string phone { get; set; } = string.Empty;
        [Required]
        public string address { get; set; } = string.Empty;
        [Required]
        public string city { get; set; } = string.Empty;
        [Required]
        public string state { get; set; } = string.Empty;
        [Required]
        public string zip { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("emailAddress")]
        public string email_address { get; set; } = string.Empty;

        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = [];
    }
}
