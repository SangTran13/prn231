using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class OrderReq
    {
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public string Freight { get; set; } = string.Empty;
        [Required]
        public int MemberId { get; set; }
    }
}
