using System.Threading.Tasks;
using RateLimiterAPI.Models;

namespace RateLimiterAPI.BL.Interfaces
{
    public interface IRateLimiter
    {
        Task<LimitResult> GetLimitStatusByIdentifier(string limitIdentifier);
    }
}
