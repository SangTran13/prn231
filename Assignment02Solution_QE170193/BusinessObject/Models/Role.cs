using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }
        [Required]
        public string role_desc { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = [];
    }
}
