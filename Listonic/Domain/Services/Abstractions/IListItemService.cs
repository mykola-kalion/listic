using System.Threading.Tasks;
using Common.Communication;
using Listonic.Domain.Models;

namespace Listonic.Domain.Services.Abstractions;

public interface IListItemService
{
    Task<StandardResponse<ListItem>> SaveAsync(ListItem item);
}