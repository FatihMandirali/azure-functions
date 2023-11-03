namespace Azure.HttpFunctionApp;

public class Product:Azure.Data.Tables.ITableEntity
{
    public string Name { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}