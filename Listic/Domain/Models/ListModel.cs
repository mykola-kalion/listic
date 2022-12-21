using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Listonic.Domain.Models
{
    [Table("Lists")]
    public sealed class ListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ListItem> Items { get; set; } = new List<ListItem>();
        public ICollection<UsersLists> Owners { get; set; }
    }
}