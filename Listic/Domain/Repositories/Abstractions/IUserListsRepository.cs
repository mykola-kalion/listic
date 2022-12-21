using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;

namespace Listonic.Domain.Repositories.Abstractions;

public interface IUserListsRepository: IRepository<UsersLists>
{
    Task<List<ListModel>> GetAllUsersLists(string userId);
}