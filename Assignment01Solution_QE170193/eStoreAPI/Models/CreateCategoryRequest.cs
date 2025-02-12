using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.Models
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Category name must be between 1 and 40 characters.")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 255 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
