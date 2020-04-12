
namespace RateLimiter.Business.BL
{
    public class BaseRateLimiter
    {
        protected virtual string GetIdentifierHash(string limitIdentifier)
        {
            return HashHelper.GetMd5Hash(limitIdentifier);
        }
    }
}
