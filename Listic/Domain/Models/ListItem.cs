namespace Listonic.Domain.Models;

public sealed class ListItem
{
    public int Id { get; set; }
    public int ListId { get; set; }
    public ListModel ListModel { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
}