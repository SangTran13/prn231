namespace DataTransfer
{
    public class OrderRequest
    {
        public string Freight { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal Discount { get; set; } = 0;
    }
}
