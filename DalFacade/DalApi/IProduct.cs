using DO;
namespace DalApi;

public interface IProduct : ICrud<Product>
{
    int Add(global::BO.Product p);
}
