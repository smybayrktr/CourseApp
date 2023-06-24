using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using CourseApp.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CourseApp.Infrastructure.Repositories.Dapper
{
    //App.settingse bağlantı yolu koy
	public class DapperRepository<T>: IRepository<T> where T: class, IEntity, new()
	{
         private string _tableName;

       private SqlConnection SqlConnection()
            {
                IConfiguration configuration = new ConfigurationBuilder()
                                                   .AddJsonFile("appsettings.json", true, true)
                                                   .Build();

       string connectionString = configuration.GetSection("CustomApplicationSettings").GetSection("connectionString").Value;
       return new SqlConnection(connectionString);

       }

       private IDbConnection CreateConnection()
       {
           var conn = SqlConnection();
           conn.Open();
           return conn;
       }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        public async Task CreateAsync(T entity)
        {
            string tableName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                string columns = string.Join(", ", entity.GetType().GetProperties().Select(p => p.Name));
                string values = string.Join(", ", entity.GetType().GetProperties().Select(p => "@" + p.Name));

                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values});";

                await connection.ExecuteAsync(query, entity);
            }
        }
        public async Task DeleteAsync(int id)
        {
            string tableName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                string query = $"DELETE FROM {tableName} WHERE Id = @Id;";

                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public T? Get(int id)
        {
            string tableName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                string query = $"SELECT * FROM {tableName} WHERE Id = @Id;";

                return connection.QuerySingleOrDefault<T>(query, new { Id = id });
            }
        }

        public IList<T?> GetAll()
        {
            string tableName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                string query = $"SELECT * FROM {tableName};";

                return connection.Query<T>(query).ToList();
            }
        }

        public async Task<IList<T?>> GetAllAsync()
        {
            string tableName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                string query = $"SELECT * FROM {tableName};";

                var result = await connection.QueryAsync<T>(query);
                return result.ToList();
            }
        }

        public IList<T> GetAllWithPredicate(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetAsync(int id)
        {
            string tableName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                string query = $"SELECT * FROM {tableName} WHERE Id = @Id;";

                return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
            }
        }

        public async Task UpdateAsync(T entity)
        {
            string tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();

            var updateColumns = properties.Select(p => $"{p.Name} = @{p.Name}").ToList();
            var query = $"UPDATE {tableName} SET {string.Join(", ", updateColumns)} WHERE Id = @Id;";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(query, entity);
            }
        }

        public Task<bool> IsExitsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

