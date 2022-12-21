using System;
using System.Threading.Tasks;
using Common.Communication;
using Listonic.Domain.Models;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Domain.Services.Abstractions;

namespace Listonic.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StandardResponse<Item>> SaveAsync(Item item)
        {
            try
            {
                await _itemRepository.CreateAsync(item);
                await _unitOfWork.CompleteAsync();

                return new StandardResponse<Item>(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StandardResponse<Item>($"An error was occured due the creating of item: {e.Message}");
            }
        }

        public async Task<StandardResponse<Item>> GetOrCreate(string itemName)
        {
            var existingItem = await _itemRepository.GetByNameAsync(itemName);

            if (existingItem != null)
            {
                return new StandardResponse<Item>(existingItem);
            }

            return await SaveAsync(new Item { Name = itemName });
        }
    }
}