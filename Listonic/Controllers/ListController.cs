using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Listonic.Domain.Models;
using Listonic.Domain.Services.Abstractions;
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
        private readonly IItemService _itemService;
        private readonly IListItemService _listItemService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public ListController(IListService listService, IItemService itemService, IMapper mapper,
            IListItemService listItemService, UserManager<IdentityUser> userManager)
        {
            _listService = listService;
            _itemService = itemService;
            _listItemService = listItemService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<ListResource>> GetAllAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _listService.ListAsync();
            return _mapper.Map<IEnumerable<ListModel>, IEnumerable<ListResource>>(
                result.Where(q => q.Owners.Select(x => x.OwnerId).Contains(userId))
            );
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
            ;
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