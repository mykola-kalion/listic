using System.Threading.Tasks;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Persistence.Contexts;

namespace Listonic.Domain.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ListonicDbContext _context;

        public UnitOfWork(ListonicDbContext context)
        {
            _context = context;
        }
        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        
    }
}