using APIBasic.DTOs;
using System.Security.Claims;

namespace APIBasic.Utilities
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static TokenInfo GetTokenInfo()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                throw new ArgumentNullException(nameof(claimsIdentity), "Not Authenticated");
            }

            return StringHelper.GetTokenInfo(claimsIdentity);
        }
    }
}
