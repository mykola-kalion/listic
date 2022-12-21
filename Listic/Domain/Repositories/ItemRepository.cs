using System.Threading.Tasks;
using Common.Repositories;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Listonic.Domain.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ListonicDbContext dbContext) : base(dbContext.Items)
        {
        }

        public async Task<Item> GetByNameAsync(string name)
        {
            return await _context.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}