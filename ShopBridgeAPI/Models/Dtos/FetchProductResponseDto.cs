namespace ShopBridgeAPI.Models.Dtos
{
    public class FetchProductResponseDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public string CategoryName { get; set; }
    }
}
