using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        [Required]
        public string email_address { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required]
        public string source { get; set; } = string.Empty;
        [Required]
        public string first_name { get; set; } = string.Empty;
        [Required]
        public string middle_name { get; set; } = string.Empty;
        [Required]
        public string last_name { get; set; } = string.Empty;
        [Required]
        public int role_id { get; set; }
        [Required]
        public int pub_id { get; set; }
        [Required]
        public DateTime hire_date { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Publisher Publisher { get; set; } = null!;
    }
}
