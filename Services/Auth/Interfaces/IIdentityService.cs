using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebTimer.Services.Auth.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityUser> GetCurrentUserAsync(ClaimsPrincipal user);
        string GetUserId();
    }
}
