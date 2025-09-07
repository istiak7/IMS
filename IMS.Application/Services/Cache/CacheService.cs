using IMS.Application.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IMS.Application.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task SetDataAsync(string cachekey, object value, TimeSpan duration)
        {
            var data = JsonSerializer.Serialize(value);

            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(duration);

            await _distributedCache.SetStringAsync(cachekey, data, cacheOptions);
        }

        public async Task<T> GetDataAsync<T>(string cachekey)
        {
            var cacheData = await _distributedCache.GetStringAsync(cachekey);
            
            if(cacheData is not null)
            {
                var data = JsonSerializer.Deserialize<T>(cacheData);

                return data;
            }

            else
            {
                _distributedCache.RemoveAsync(cachekey);
            }
            return default;
        }

        public async Task RemoveDataAsync(string cachekey)
        {

            var cacheData = await _distributedCache.GetStringAsync(cachekey);

            if(cacheData is not null)
            {
                await _distributedCache.RemoveAsync(cachekey);
            }
            
        }
    }
}
