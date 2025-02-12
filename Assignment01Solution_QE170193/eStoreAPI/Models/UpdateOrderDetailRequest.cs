using System.Text.Json.Serialization;

namespace eStoreAPI.Models
{
    public class UpdateOrderDetailRequest
    {
        [JsonRequired]
        public decimal UnitPrice { get; set; }
        [JsonRequired]
        public int Quantity { get; set; }
        [JsonRequired]
        public decimal Discount { get; set; }
    }
}
