using System.Data;

namespace DRF.infrastructures
{
    public interface  IDapperRepository<T> : IDisposable where T : class
    {
        int Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        int Create(List<T> entities);
        bool Update(List<T> entities);
        bool Delete(List<T> entities);
        T GetById(object id);
        IEnumerable<T> GetAll();
        int Execute(string sql, object parameters, CommandType commandType);
        IEnumerable<T> Query(string sql, object parameters, CommandType commandType);
        IEnumerable<E> Query<E>(string sql, object parameters, CommandType commandType);
        E ExecuteScalar<E>(string sql, object parameters, CommandType commandType = CommandType.Text);
        T QueryFirstOrDefult(string sql, object parameters, CommandType commandType);
        E QueryFirstOrDefult<E>(string sql, object parameters, CommandType commandType);
    }
}
