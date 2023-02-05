namespace ShopBridgeAPI.Models.Dtos
{
    public class ProductRequestDto
    {
        public string ProductName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string CategoryName { get; set; }
    }
}
