using System.Text.Json.Serialization;

namespace TeaTime.Api.Models
{
    public class Order
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
