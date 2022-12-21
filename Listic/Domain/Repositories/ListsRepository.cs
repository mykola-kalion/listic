using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Listonic.Domain.Repositories
{
    public class ListsRepository : Repository<ListModel>, IListRepository
    {
        public ListsRepository(ListonicDbContext context) : base(context.Lists)
        {
        }

        public new async Task<IEnumerable<ListModel>> GetAllAsync()
        {
            return await _context
                .Include(l => l.Items)
                .ThenInclude(i => i.Item)
                .Include(x => x.Owners)
                .ThenInclude(x => x.Owner)
                .ToListAsync();
        }

        public new async Task<ListModel> GetByIdAsync(int id)
        {
            return await _context.Include(l => l.Items)
                .ThenInclude(i => i.Item)
                .Include(x => x.Owners)
                .ThenInclude(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}