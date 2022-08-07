using Listonic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Listonic.Extensions;

public static class DbExtensions
{
    public static ModelBuilder ListonicDbModels(this ModelBuilder builder)
    {
        builder.Entity<ListModel>().ToTable("Lists");
        builder.Entity<ListModel>().HasKey(p => p.Id);
        builder.Entity<ListModel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ListModel>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<ListModel>()
            .HasMany(p => p.Items)
            .WithOne(p => p.ListModel)
            .HasForeignKey(p => p.ListId);
        builder.Entity<ListModel>()
            .HasMany(p => p.Owners)
            .WithOne(p => p.ListModel)
            .HasForeignKey(p => p.OwnerId);

        builder.Entity<Item>().ToTable("Items");
        builder.Entity<Item>().HasKey(p => p.Id);
        builder.Entity<Item>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Item>().Property(p => p.Name).IsRequired().HasMaxLength(30);

        builder.Entity<ListItem>().ToTable("ListItems");
        builder.Entity<ListItem>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ListItem>().HasKey(li => new { li.ItemId, li.ListId });
        builder.Entity<ListItem>()
            .HasOne(x => x.ListModel)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ListId);

        builder.Entity<UsersLists>().ToTable("UsersLists");
        builder.Entity<UsersLists>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<UsersLists>().HasKey(ul => new { ul.ListId, ul.OwnerId });
        builder.Entity<UsersLists>()
            .HasOne(x => x.ListModel)
            .WithMany(x => x.Owners)
            .HasForeignKey(x => x.ListId);

        // builder.Entity<List>().HasData
        // (
        //     new List { Id = 100, Name = "My first list" },
        //     new List { Id = 101, Name = "My second list" }
        // );
        
        // builder.Entity<Item>().HasData(
        //     new Item { Id = 200, Name = "Carrot" });
        
        // builder.Entity<ListItem>().HasData(
        //     new ListItem { Id = 1, ItemId = 200, ListId = 100, Quantity = 5 });

        return builder;
    }
}