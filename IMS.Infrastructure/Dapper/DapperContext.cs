using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Dapper
{
    public class DapperContext : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        private readonly IDbConnection _dbConnection;
        public DapperContext(IConfiguration config)
        {
            _config = config;
            _dbConnection = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
        }

       public async Task<IEnumerable<T>> GetAllAsync<T>(string sqlQuery, object parameters = null, CommandType commandType = CommandType.Text)
       {
            return await _dbConnection.QueryAsync<T>(sqlQuery, parameters, commandType:commandType);
       }

       public async Task<T> GetByIdAsync<T>(string sqlQuery, object parameters = null, CommandType commandType = CommandType.Text)
       {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sqlQuery, parameters, commandType: commandType);
       }
    }
}
