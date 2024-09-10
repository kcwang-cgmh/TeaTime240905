namespace TeaTime.Api.Domains.Store
{
    public class StoreDTO
    {
        // 創建商店時所需的數據
        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string MenuUrl { get; set; } = string.Empty;
    }
}
