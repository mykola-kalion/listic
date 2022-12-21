using System.Linq;
using Listonic.Domain.Models;

namespace Listonic.Extensions;

public static class ListModelExtensions
{
    public static bool IsOwner(this ListModel listModel, string userId)
    {
        return listModel.Owners.Select(x => x.OwnerId).Contains(userId);
    }
}