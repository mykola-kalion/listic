using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Listonic.Domain.Models;
using Listonic.Domain.Services.Abstractions;
using Listonic.Extensions;
using Listonic.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Listonic.Controllers;

public abstract class AbstractListController : Controller
{
    private readonly IListService _listService;
    private readonly IListItemService _listItemService;
    private readonly IMapper _mapper;

    protected AbstractListController(IListService listService, IMapper mapper,
        IListItemService listItemService)
    {
        _listService = listService;
        _listItemService = listItemService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ListResource>> GetAllAsync(string userId)
    {
        var result = await _listService.ListAsync();
        return _mapper.Map<IEnumerable<ListModel>, IEnumerable<ListResource>>(
            result.Where(q => q.IsOwner(userId))
        );
    }

    
    public async Task<IActionResult> GetByIdAsync(string userId, int listId)
    {
        var list = await _listService.GetByIdAsync(listId);

        if (!list.IsOwner(userId))
        {
            return NotFound();
        }

        var result = _mapper.Map<ListModel, ListResource>(list);

        return Ok(result);
    }
    
    public async Task<IActionResult> PostAsync(CreateListResource resource, string userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }
        
        var list = _mapper.Map<CreateListResource, ListModel>(resource);

        var result = await _listService.SaveAsync(list, userId);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var listResource = _mapper.Map<ListModel, ListResource>(result.Obj);
        return Ok(listResource);
    }
    
    public async Task<IActionResult> UpdateAsync(int listId, CreateListResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }

        var list = _mapper.Map<CreateListResource, ListModel>(resource);
        var result = await _listService.UpdateAsync(listId, list);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var listResource = _mapper.Map<ListModel, ListResource>(result.Obj);
        return Ok(listResource);
    }
    
    public async Task<IActionResult> AddItemAsync(string userId, int listId, [FromBody] AddItemToListResource itemListResource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }
        
        var list = await _listService.GetByIdAsync(listId);

        if (!list.IsOwner(userId))
        {
            return NotFound();
        }

        var item = _mapper.Map<AddItemToListResource, ListItem>(itemListResource);
        item.ListId = listId;
        var result = await _listItemService.SaveAsync(item);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        list.Items.Add(item);
        
        return Ok(_mapper.Map<ListModel, ListResource>(list));
    }
}