using System.ComponentModel.DataAnnotations.Schema;

namespace Listonic.Domain.Models
{
    [Table("Items")]
    public sealed class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}