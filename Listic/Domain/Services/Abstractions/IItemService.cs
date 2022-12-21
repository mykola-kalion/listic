using System.Threading.Tasks;
using Common.Communication;
using Listonic.Domain.Models;

namespace Listonic.Domain.Services.Abstractions
{
    public interface IItemService
    {
        Task<StandardResponse<Item>> SaveAsync(Item item);
        Task<StandardResponse<Item>> GetOrCreate(string itemName);
    }
}