using System.Threading.Tasks;

namespace Listonic.Domain.Repositories.Abstractions
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}