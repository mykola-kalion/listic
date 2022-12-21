using System.Threading.Tasks;
using Common.Communication;
using Listonic.Domain.Models;

namespace Listonic.Domain.Services.Abstractions;

public interface IUserListService
{
    Task<StandardResponse<UsersLists>> AddListOwnership(int listId, string userId);
}