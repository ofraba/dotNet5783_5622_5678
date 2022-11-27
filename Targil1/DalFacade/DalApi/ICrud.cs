using System;

public interface ICrud<T>
{
    public int Add(T entity);
    public void Update(T entity);
    public void Delete(int idProduct);
    public T Get(int idOrderItem);
}
