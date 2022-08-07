using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Domain.Services.Abstractions;
using Listonic.Domain.Services.Communication;

namespace Listonic.Domain.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;
        private readonly IUserListService _userListService;
        private readonly IUnitOfWork _unitOfWork;

        public ListService(IListRepository listRepository, IUnitOfWork unitOfWork, IUserListService userListService)
        {
            _listRepository = listRepository;
            _unitOfWork = unitOfWork;
            _userListService = userListService;
        }

        public async Task<IEnumerable<ListModel>> ListAsync()
        {
            return await _listRepository.GetAllAsync();
        }

        public async Task<StandardResponse<ListModel>> SaveAsync(ListModel listModel, string userId)
        {
            try
            {
                var list = await _listRepository.CreateAsync(listModel);
                await _unitOfWork.CompleteAsync();
                await _userListService.AddListOwnership(list.Entity.Id, userId);

                return new StandardResponse<ListModel>(listModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StandardResponse<ListModel>($"An error was occured due the creating of list: {e.Message}");
            }
        }

        public async Task<StandardResponse<ListModel>> UpdateAsync(int listId, ListModel listModel)
        {
            var existingList = await _listRepository.GetByIdAsync(listId);

            if (existingList == null)
            {
                return new StandardResponse<ListModel>("List not found.");
            }

            existingList.Name = listModel.Name;

            try
            {
                _listRepository.Update(existingList);
                await _unitOfWork.CompleteAsync();

                return new StandardResponse<ListModel>(existingList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StandardResponse<ListModel>($"An error occured due updating the list: {e.Message}");
            }
        }
    }
}