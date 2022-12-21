using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Listonic.Domain.Repositories;

public class ListItemRepository: Repository<ListItem>, IListItemRepository
{
    public ListItemRepository(ListonicDbContext dbContext) : base(dbContext.ListItems)
    {
    }

    public new async Task<IEnumerable<ListItem>> GetAllAsync()
    {
       return await _context
           .Include(x => x.ListModel)
           .ToListAsync();
    }

    public new async Task<EntityEntry<ListItem>> CreateAsync(ListItem obj)
    {
        return await _context.AddAsync(obj);
    }

    public async Task<ListItem> GetByKeysAsync(int listId, int itemId)
    {
        return await _context
            .Include(x => x.ListModel)
            .ThenInclude(x => x.Items)
            .ThenInclude(x => x.Item)
            .SingleOrDefaultAsync(x => x.ListId == listId && x.ItemId == itemId);
    }
}