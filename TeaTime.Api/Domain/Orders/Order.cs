namespace TeaTime.Api.Domain.Orders
{
    public class Order
    {
        public long Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
