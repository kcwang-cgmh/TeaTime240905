namespace TeaTime.Api.DataAccess.DBEntities
{
    public class StoreEntity
    {
        // 數據層的實體，對應於數據庫中的表結構
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string MenuUrl { get; set; } = string.Empty;
    }
}
