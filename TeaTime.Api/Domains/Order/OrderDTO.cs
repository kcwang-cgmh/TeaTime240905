namespace TeaTime.Api.Domains.Order
{
    public class OrderDTO
    {
        // 創建訂單時所需的數據
        public string UserName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
    }
}
