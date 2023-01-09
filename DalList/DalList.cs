using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    DalList()
    {

    }
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrders();
    public IOrderItem OrderItem => new DalOrderItem();
}
