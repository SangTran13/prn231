using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required, StringLength(40)]
        public string CategoryName { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
