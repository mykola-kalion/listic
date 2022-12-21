using Listonic.Domain.Repositories;
using Listonic.Domain.Repositories.Abstractions;
using Listonic.Domain.Services;
using Listonic.Domain.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Listonic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddListonicServices(this IServiceCollection services)
    {
        services.AddScoped<IListRepository, ListsRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IListItemRepository, ListItemRepository>();
        services.AddScoped<IUserListsRepository, UserListsRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IListService, ListService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IListItemService, ListItemService>();
        services.AddScoped<IUserListService, UserListsService>();

        return services;
    }
}