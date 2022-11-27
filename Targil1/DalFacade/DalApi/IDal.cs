using DO;
namespace DalApi;

public interface IDal
{
    public IProduct product{get;}
    public IOrder order { get; }
    public IOrderItem product { get; }
}



