using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace DRF.infrastructures
{
    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        private readonly string strConnection;
        public DapperRepository(string strConnection)
        {
            this.strConnection = strConnection;
        }
        public int Create(T entity)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                var s = connection.Insert<T>(entity);
                return Convert.ToInt32(s);
            }
        }
        public int Create(List<T> entities)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return Convert.ToInt32(connection.Insert(entities));
            }
        }
        public bool Delete(T entity)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Delete<T>(entity);
            }
        }
        public bool Delete(List<T> entities)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Delete(entities);
            }
        }
        public void Dispose()
        {

        }
        public int Execute(string sql, object parameters, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Execute(sql, parameters, commandType: commandType);
            }
        }
        public E ExecuteScalar<E>(string sql, object parameters, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.ExecuteScalar<E>(sql, parameters, commandType: commandType);
            }
        }
        public IEnumerable<T> GetAll()
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.GetAll<T>();
            }
        }
        public T GetById(object id)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Get<T>(id);
            }
        }
        public IEnumerable<T> Query(string sql, object parameters, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Query<T>(sql, parameters, commandType: commandType);

            }
        }
        public IEnumerable<E> Query<E>(string sql, object parameters, CommandType commandType)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Query<E>(sql, parameters, commandType: commandType);
            }
        }

        public T QueryFirstOrDefult(string sql, object parameters, CommandType commandType)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.QueryFirstOrDefault<T>(sql, parameters, commandType: commandType);

            }
        }

        public E QueryFirstOrDefult<E>(string sql, object parameters, CommandType commandType)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.QueryFirstOrDefault<E>(sql, parameters, commandType: commandType);
            }
        }

        public bool Update(T entity)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                try
                {
                    return connection.Update<T>(entity);

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool Update(List<T> entities)
        {
            using (var connection = new SqlConnection(strConnection))
            {
                return connection.Update(entities);
            }
        }
    }
}
