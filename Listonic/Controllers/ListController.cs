using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Listonic.Domain.Models;
using Listonic.Domain.Services.Abstractions;
using Listonic.Extensions;
using Listonic.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Listonic.Controllers
{
    [Route("/api/[controller]")]
    public class ListController : Controller
    {
        private readonly IListService _listService;
        private readonly IListItemService _listItemService;
        private readonly IMapper _mapper;

        public ListController(IListService listService, IItemService itemService, IMapper mapper,
            IListItemService listItemService, UserManager<IdentityUser> userManager)
        {
            _listService = listService;
            _listItemService = listItemService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<ListResource>> GetAllAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _listService.ListAsync();
            return _mapper.Map<IEnumerable<ListModel>, IEnumerable<ListResource>>(
                result.Where(q => q.IsOwner(userId))
            );
        }
        
        [HttpGet("{listId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByIdAsync(int listId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await _listService.GetByIdAsync(listId);

            if (!list.IsOwner(userId))
            {
                return NotFound();
            }
            
            var result = _mapper.Map<ListModel, ListResource>(list);
            
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostAsync([FromBody] CreateListResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _mapper.Map<CreateListResource, ListModel>(resource);
            
            var result = await _listService.SaveAsync(list, userId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var listResource = _mapper.Map<ListModel, ListResource>(result.Obj);
            return Ok(listResource);
        }

        [HttpPatch("{listId}")]
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

        [HttpPost("{listId}/addItem")]
        public async Task<IActionResult> AddItemAsync(int listId, [FromBody] AddItemToListResource itemListResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var item = _mapper.Map<AddItemToListResource, ListItem>(itemListResource);
            item.ListId = listId;
            var result = await _listItemService.SaveAsync(item);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var updatedList = _listService.ListAsync().Result.Single(x => x.Id == listId);

            var list = _mapper.Map<ListModel, ListResource>(updatedList);
            return Ok(list);
        }
    }
}