namespace TeaTime.Api.DataAccess.DbEntity
{
    public class OrderEntity
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
    }
}
