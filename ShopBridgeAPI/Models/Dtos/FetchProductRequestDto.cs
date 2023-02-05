using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShopBridgeAPI.Models.Enumerations;

namespace ShopBridgeAPI.Models.Dtos
{
    public class FetchProductRequestDto
    {
        public int ProductId { get; set; }

        public string? SearchTerm { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderByColumn SortProperty { get; set; }

        public string? SortDirection { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public List<string>? Categories { get; set; }
    }
}
