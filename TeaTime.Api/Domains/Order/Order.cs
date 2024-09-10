namespace TeaTime.Api.Domains.Order
{
    public class Order
    {
        // 業務處理需要的資料
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
