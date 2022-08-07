using System.Threading.Tasks;
using Listonic.Domain.Models;
using Listonic.Domain.Services.Communication;

namespace Listonic.Domain.Services.Abstractions;

public interface IListItemService
{
    Task<StandardResponse<ListItem>> SaveAsync(ListItem item);
}