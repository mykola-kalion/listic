using Microsoft.AspNetCore.Identity;

namespace Common.Models;

public class TelegramUser: IdentityUser
{
    public int? TelegramId { get; set; }
}