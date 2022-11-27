using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi;

public interface ICart
{
    public void Confirm(Cart c,string name,string email,string address);
    public Cart AddProductToCart(Cart c,int productId);
    public Cart Update(Cart c, int productId,int amount);
}
