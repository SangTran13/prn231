using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.Models
{
    public class UpdateSupplierRequest
    {
        [Required(ErrorMessage = "Supplier Name is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Supplier name must be between 1 and 40 characters.")]
        public string SupplierName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supplier Address is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Supplier address must be between 1 and 255 characters.")]
        public string SupplierAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telephone is required.")]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Telephone must be between 1 and 12 characters.")]
        public string Telephone { get; set; } = string.Empty;
    }
}
