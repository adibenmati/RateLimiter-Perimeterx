using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RateLimiter.Common.Models;

namespace RateLimiter.Business.BL.Interfaces
{
    public interface IRateLimiter
    {
        Task<LimitResult> GetLimitStatusByIdentifier(string limitIdentifier);
    }
}
