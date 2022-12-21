using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Listonic.Domain.Services.Abstractions;
using Listonic.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Listonic.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("/api/[controller]")]
public class ListController : AbstractListController
{
    public ListController(IListService listService, IMapper mapper, IListItemService listItemService) : base(
        listService, mapper, listItemService)
    {
    }

    // double async - do this has effect?
    [HttpGet]
    public async Task<IEnumerable<ListResource>> GetAllAsync()
    {
        var userId = GetUser();
        return await base.GetAllAsync(userId);
    }

    [HttpGet("{listId}")]
    public async Task<IActionResult> GetByIdAsync(int listId)
    {
        var userId = GetUser();
        return await base.GetByIdAsync(userId, listId);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateListResource resource)
    {
        var userId = GetUser();
        return await base.PostAsync(resource, userId);
    }


    private string GetUser()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}