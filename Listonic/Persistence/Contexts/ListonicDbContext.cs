using Listonic.Domain.Models;
using Listonic.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Listonic.Persistence.Contexts
{
    public class ListonicDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ListModel> Lists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<UsersLists> UsersLists { get; set; }

        public ListonicDbContext(DbContextOptions<ListonicDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ListonicDbModels();
        }
    }
}