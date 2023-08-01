using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebTimer.Services.Auth.Interfaces;

namespace WebTimer.Services.Auth
{
    public class IdentityService : IIdentityService
    {
        readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public IdentityService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityUser> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            try
            {
                var username = user.Identity.Name;

                var userIdentity = await userManager.FindByNameAsync(username);

                if (userIdentity == null)
                    throw new ApplicationException("Usuário não encontrado");

                return userIdentity;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Usuário não encontrado");
            }
        }

        public string GetUserId()
        {
            return httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
