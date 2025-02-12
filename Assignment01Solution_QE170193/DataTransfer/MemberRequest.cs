using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class MemberRequest
    {
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
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}