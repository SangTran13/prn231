using System.ComponentModel.DataAnnotations;

namespace eBookStoreWebAPI.ViewModels
{
    public class PublisherVM
    {
        [Required]
        public string PublisherName { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string State { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
    }
}
