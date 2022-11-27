using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi;

public interface ICart
{
    public Product Confirm(Cart c,string name,string email,string address);
    public int Add(Cart c,int productId);
    public void Update(Cart c, int productId,int amount);
}
