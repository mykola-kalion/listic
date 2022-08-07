using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Listonic.Domain.Models;

public sealed class UsersLists
{
    public int Id { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public int ListId { get; set; }
    public ListModel ListModel { get; set; }

}