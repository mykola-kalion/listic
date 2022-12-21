using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models;
using Listonic.Domain.Services.Abstractions;
using Listonic.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Listonic.Controllers;

[Route("/api/telegram/{telegramId:int}")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TelegramBot")]
public class TelegramListController : AbstractListController
{
    private readonly UserManager<TelegramUser> _userManager;

    public TelegramListController(IListService listService, IMapper mapper,
        IListItemService listItemService, UserManager<TelegramUser> userManager) : base(listService,
        mapper, listItemService)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public Task<IEnumerable<ListResource>> GetAllAsync(int telegramId)
    {
        var userId = GetUser(telegramId);
        return base.GetAllAsync(userId);
    }
    
    [HttpGet("{listId}")]
    public Task<IActionResult> GetByIdAsync(int telegramId, int listId)
    {
        var userId = GetUser(telegramId);
        return base.GetByIdAsync(userId, listId);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(int telegramId, [FromBody] CreateListResource resource)
    {
        var userId = GetUser(telegramId);
        return await base.PostAsync(resource, userId);
    }

    [HttpPost("{listId}")]
    public async Task<IActionResult> AddItemAsync(int telegramId, int listId, [FromBody] AddItemToListResource itemListResource)
    {
        var userId = GetUser(telegramId);
        return await base.AddItemAsync(userId, listId, itemListResource);
    }

    private string GetUser(int telegramId)
    {
        return _userManager.Users.Single(x => x.TelegramId == telegramId).Id;
    }
}