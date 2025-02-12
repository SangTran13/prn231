using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class Member
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        [Required, StringLength(40)]
        public string MemberName { get; set; } = string.Empty;
        [Required, StringLength(40)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(40)]
        public string City { get; set; } = string.Empty;
        [Required, StringLength(40)]
        public string Country { get; set; } = string.Empty;
        [MinLength(5)]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;
        [Required, StringLength(10)]
        public string Role { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
