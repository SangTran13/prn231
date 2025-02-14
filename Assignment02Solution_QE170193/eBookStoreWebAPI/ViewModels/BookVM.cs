using System.ComponentModel.DataAnnotations;

namespace eBookStoreWebAPI.ViewModels
{
    public class BookVM
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Advance { get; set; } = string.Empty;
        [Required]
        public double Royalty { get; set; }
        [Required]
        public int YtdSales { get; set; }
        [Required]
        public string Notes { get; set; } = string.Empty;
        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
