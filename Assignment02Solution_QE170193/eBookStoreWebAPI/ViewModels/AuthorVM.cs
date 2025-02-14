using System.ComponentModel.DataAnnotations;

namespace eBookStoreWebAPI.ViewModels
{
    public class AuthorVM
    {
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string State { get; set; } = string.Empty;
        [Required]
        public string Zip { get; set; } = string.Empty;
        [Required]
        public string EmailAddress { get; set; } = string.Empty; 
    }
}
