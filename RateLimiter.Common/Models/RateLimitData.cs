using System;

namespace RateLimiter.Common.Models
{
    public class RateLimitData
    {
        public RateLimitData(int timeToLive)
        {
            Count = 1;
            CloseWindowTime = DateTime.UtcNow.AddMilliseconds(timeToLive);
        }
        public DateTime CloseWindowTime { get; set; }

        public int Count { get; set; }
    }
}
