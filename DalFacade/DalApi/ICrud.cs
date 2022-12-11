using DO;
using System;
namespace DalApi;

public interface ICrud<T>
{
    public int Add(T entity);
    public void Update(T entity);
    public void Delete(int idProduct);
    public T Get(int idOrderItem);
    public IEnumerable<T> GetAll(Func<T, bool> func = null);
}
