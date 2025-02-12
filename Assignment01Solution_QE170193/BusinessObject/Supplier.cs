using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
        [Required, StringLength(40)]
        public string SupplierName { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string SupplierAddress { get; set; } = string.Empty;
        [Required, StringLength(12)]
        public string Telephone { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
