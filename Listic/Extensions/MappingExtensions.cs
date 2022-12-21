using AutoMapper;
using Listonic.Domain.Models;
using Listonic.Resources;

namespace Listonic.Extensions
{
    public static class ModelToResourceProfile
    {
        public static Profile ListonicMappings(this Profile profile)
        {
            profile.CreateMap<Item, ItemResource>();
            profile.CreateMap<ListModel, ListResource>();
            profile.CreateMap<ListItem, ItemResource>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Item.Name));
            
            profile.CreateMap<CreateListResource, ListModel>();
            profile.CreateMap<AddItemToListResource, ListItem>()
                .ForPath(dest => dest.Item.Name, 
                    opt => opt.MapFrom(src => src.Name));

            return profile;
        }
    }
}