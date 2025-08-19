using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Dapper
{
    public interface IDapper
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string sqlQuery, object parameters = null, CommandType commandType = CommandType.Text);
        Task<T> GetByIdAsync<T>(string sqlQuery, object parameters = null, CommandType commandType = CommandType.Text);
    }
}
