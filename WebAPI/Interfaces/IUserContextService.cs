using System.Security.Claims;

namespace WebAPI.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}