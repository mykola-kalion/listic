using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Communication;
using Listonic.Domain.Models;

namespace Listonic.Domain.Services.Abstractions
{
    public interface IListService
    {
        Task<IEnumerable<ListModel>> ListAsync();
        Task<StandardResponse<ListModel>> SaveAsync(ListModel listModel, string userId);
        Task<StandardResponse<ListModel>> UpdateAsync(int listId, ListModel listModel);
        Task<ListModel> GetByIdAsync(int listId);
    }
}