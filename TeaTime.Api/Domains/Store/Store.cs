namespace TeaTime.Api.Domains.Store
{
    public class Store
    {
        // 業務處理需要的資料
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string MenuUrl { get; set; } = string.Empty;

    }
}
