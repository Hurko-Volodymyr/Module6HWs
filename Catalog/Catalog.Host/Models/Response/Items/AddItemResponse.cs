namespace Catalog.Host.Models.Response.Items;

public class AddItemResponse<T>
{
    public T Id { get; set; } = default!;
}