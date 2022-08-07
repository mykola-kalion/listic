using System;
using System.Threading.Tasks;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Domain.Services.Abstractions;
using Listonic.Domain.Services.Communication;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Listonic.Domain.Services
{
    public class ListItemService : IListItemService
    {
        private readonly IListItemRepository _listItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemService _itemService;

        public ListItemService(
            IListItemRepository listItemRepository,
            IUnitOfWork unitOfWork,
            IItemService itemService
        )
        {
            _listItemRepository = listItemRepository;
            _unitOfWork = unitOfWork;
            _itemService = itemService;
        }

        public async Task<StandardResponse<ListItem>> SaveAsync(ListItem item)
        {
            var existingItem = await _itemService.GetOrCreate(item.Item.Name);
            var existingListItem = await _listItemRepository.GetByKeysAsync(item.ListId, existingItem.Obj.Id);

            if (existingListItem == null)
            {
                try
                {
                    EntityEntry<ListItem> listItem = await _listItemRepository.CreateAsync(new ListItem
                        { ListId = item.ListId, ItemId = existingItem.Obj.Id, Quantity = item.Quantity });
                    await _unitOfWork.CompleteAsync();

                    return new StandardResponse<ListItem>(listItem.Entity);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new StandardResponse<ListItem>(
                        $"An error was occured due the creating of list: {e.Message}");
                }
            }

            try
            {
                existingListItem.Quantity += item.Quantity;
                await _unitOfWork.CompleteAsync();

                return new StandardResponse<ListItem>(existingListItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StandardResponse<ListItem>($"An error was occured due adding item to list: {e.Message}");
            }
        }
    }
}