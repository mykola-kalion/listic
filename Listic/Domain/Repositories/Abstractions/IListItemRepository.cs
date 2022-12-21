using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;

namespace Listonic.Domain.Repositories.Abstractions;

public interface IListItemRepository: IRepository<ListItem>
{
    Task<ListItem> GetByKeysAsync(int listId, int itemId);
}