using System.Threading.Tasks;
using Listonic.Domain.Models;
using Listonic.Domain.Services.Communication;

namespace Listonic.Domain.Services.Abstractions;

public interface IUserListService
{
    Task<StandardResponse<UsersLists>> AddListOwnership(int listId, string userId);
}