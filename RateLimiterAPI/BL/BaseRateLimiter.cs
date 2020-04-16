
using RateLimiterAPI.Helpers;

namespace RateLimiterAPI.BL
{
    public class BaseRateLimiter
    {
        protected virtual string GetIdentifierHash(string limitIdentifier)
        {
            return HashHelper.GetMd5Hash(limitIdentifier);
        }
    }
}
