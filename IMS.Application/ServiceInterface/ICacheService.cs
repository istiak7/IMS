using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.ServiceInterface
{
    public interface ICacheService
    {
        Task SetDataAsync(string cachekey, object value, TimeSpan duration);
        Task RemoveDataAsync(string cachekey);
        Task<T> GetDataAsync<T>(string cachekey);
    }
}
