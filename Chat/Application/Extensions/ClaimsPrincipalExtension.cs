using System.Security.Claims;

namespace Application.Extentions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
