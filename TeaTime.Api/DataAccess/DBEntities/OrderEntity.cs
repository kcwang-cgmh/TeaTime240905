namespace TeaTime.Api.DataAccess.DBEntities
{
    public class OrderEntity
    {
        // 數據層的實體，對應於數據庫中的表結構
        public long Id { get; set; }

        public long StoreId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;
    }
}
