using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Persistence.Contexts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Listonic.Domain.Repositories;

public class UserListsRepository: Repository<UsersLists>, IUserListsRepository
{
    public UserListsRepository(ListonicDbContext context): base(context.UsersLists)
    {
    }

    public async Task<List<ListModel>> GetAllUsersLists(string userId)
    {
        return await _context.Where(x => x.OwnerId == userId).Select(x => x.ListModel).ToListAsync();
    }
}