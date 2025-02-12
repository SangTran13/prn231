using System.Text.Json.Serialization;

namespace eStoreAPI.Models
{
    public class CreateOrderDetailRequest
    {
        [JsonRequired]
        public int OrderId { get; set; }
        [JsonRequired]
        public int ProductId { get; set; }
        [JsonRequired]
        public decimal UnitPrice { get; set; }
        [JsonRequired]
        public int Quantity { get; set; }
        [JsonRequired]
        public decimal Discount { get; set; }
    }
}
