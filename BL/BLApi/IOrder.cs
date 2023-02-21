using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BLApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetAll(Func<BO.OrderForList, bool>? func = null);
    public Order GetForManegar(int idOrder);
    //עדכון שילוח הזמנה
    public Order OrderShippingUpdate(int idOrder);
    //עדכון אספקת הזמנה
    public Order OrderDeliveryUpdate(int idOrder);

    //מעקב הזמנה
    public OrderTracking OrderTracking(int idOrder);
    public int? ChooseOrder();

    //עדכו הזמנה-בונוס
    //public void Update();
}
