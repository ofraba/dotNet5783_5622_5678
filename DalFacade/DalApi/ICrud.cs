using DO;
using System;
namespace DalApi;

public interface ICrud<T>
{
    public int Add(T entity);
    public void Update(T entity);
    public void Delete(int id);
    public T Get(int id);
    public T Get(Predicate<T> func);
    public IEnumerable<T> GetAll(Func<T, bool> func = null);
}
