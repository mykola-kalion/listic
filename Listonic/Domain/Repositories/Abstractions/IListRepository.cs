using Common.Repositories;
using Listonic.Domain.Models;

namespace Listonic.Domain.Repositories.Abstractions
{
    public interface IListRepository: IRepository<ListModel>
    {
    }
}