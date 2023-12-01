using System.Security.Claims;


namespace BusinessLayer.Extentions
{

    public static class ClaimsPrincipalExtention
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
