using AutoMapper;
using Listonic.Extensions;
using Users.Extensions;

namespace API;

public class Mappings: Profile
{
    public Mappings()
    {
        this.ListonicMappings();
        this.AccountMappings();
    }
}