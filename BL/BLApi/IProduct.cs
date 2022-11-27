using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetAll();
    public Product GetForManegar(int idProduct);
    public ProductItem GetForClient(int idProduct,Cart c);
    public void Add(Product p);
    public void Delete(int idProduct);
    public void Update(Product p);
    
}
