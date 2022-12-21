using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;

namespace Listonic.Domain.Repositories.Abstractions
{
    public interface IItemRepository: IRepository<Item>
    {
        Task<Item> GetByNameAsync(string name);
    }
}