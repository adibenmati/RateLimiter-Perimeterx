using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RateLimiter.Business.BL.Interfaces;
using RateLimiter.Common.Models;

namespace RateLimiter.Business.BL
{
    public class ConcurrentDictionaryRateLimiter : BaseRateLimiter, IRateLimiter 
    {
        private int _threshold;
        private int _timeToLive;
        private ConcurrentDictionary<string, RateLimitData> _rateLimitsConcurrentDictionary;
        private readonly ILogger<IRateLimiter> _logger;

        public ConcurrentDictionaryRateLimiter(Microsoft.Extensions.Configuration.IConfiguration configuration, ILogger<IRateLimiter> logger)
        {
            var threshold = configuration.GetValue<int>("threshold");
            var ttl = configuration.GetValue<int>("ttl");

            if (threshold == 0 || ttl == 0)
            {
                throw new InvalidOperationException("Startup Parameters are invalid");
            }
            _threshold = threshold;
            _timeToLive = ttl;
            _logger = logger;
            _rateLimitsConcurrentDictionary = new ConcurrentDictionary<string, RateLimitData>();
        }

        public Task<LimitResult> GetLimitStatusByIdentifier(string limitIdentifier)
        {
            var result = new LimitResult()
            {
                Blocked = true
            };

            try
            {
                var hash = GetIdentifierHash(limitIdentifier);
                if (!_rateLimitsConcurrentDictionary.ContainsKey(hash))
                {
                    _rateLimitsConcurrentDictionary.TryAdd(hash, new RateLimitData(_timeToLive));
                    result.Blocked = false;
                }
                else
                {
                    _rateLimitsConcurrentDictionary.TryGetValue(hash, out var limitData);
                    if (limitData != null)
                    {
                        if (limitData.CloseWindowTime > DateTime.UtcNow)
                        {
                            if (_threshold >= ++limitData.Count)
                            {
                                result.Blocked = false;
                            }
                        }
                        else
                        {
                            var targetLimitData = new RateLimitData(_timeToLive);
                            _rateLimitsConcurrentDictionary.AddOrUpdate(hash, targetLimitData, (key, existingVal) => targetLimitData);
                            result.Blocked = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"something went wrong during rate limiter processing with exception: ${e.Message}");
            }

            return Task.FromResult(result);
        }
    }
}