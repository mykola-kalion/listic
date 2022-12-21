using System.Collections.Generic;

namespace Listonic.Resources;

public class ListResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ItemResource> Items { get; set; } = new List<ItemResource>();
}