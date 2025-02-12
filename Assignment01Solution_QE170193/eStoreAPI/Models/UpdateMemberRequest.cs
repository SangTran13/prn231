using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eStoreAPI.Models
{
    public class UpdateMemberRequest
    {
        [Required(ErrorMessage = "Member name is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Member name be between 1 and 40 characters.")]
        public string MemberName { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "City must be between 1 and 40 characters.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Country must be between 1 and 40 characters.")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5)]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [JsonRequired]
        public DateTime DateOfBirth { get; set; }
    }
}
