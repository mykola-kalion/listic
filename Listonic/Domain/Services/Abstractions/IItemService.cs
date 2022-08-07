using System.Threading.Tasks;
using Listonic.Domain.Models;
using Listonic.Domain.Services.Communication;

namespace Listonic.Domain.Services.Abstractions
{
    public interface IItemService
    {
        Task<StandardResponse<Item>> SaveAsync(Item item);
        Task<StandardResponse<Item>> GetOrCreate(string itemName);
    }
}