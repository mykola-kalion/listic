using System;
using System.Threading.Tasks;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Domain.Services.Abstractions;
using Listonic.Domain.Services.Communication;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Listonic.Domain.Services;

public class UserListsService : IUserListService
{
    private readonly IUserListsRepository _userListsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserListsService(IUserListsRepository userListsRepository, IUnitOfWork unitOfWork)
    {
        _userListsRepository = userListsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<StandardResponse<UsersLists>> AddListOwnership(int listId, string userId)
    {
        try
        {
            var result = await _userListsRepository.CreateAsync(new UsersLists { OwnerId = userId, ListId = listId });
            await _unitOfWork.CompleteAsync();
            return new StandardResponse<UsersLists>(result.Entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StandardResponse<UsersLists>(e.Message);
        }
    }
}