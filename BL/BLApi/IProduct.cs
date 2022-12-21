using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetAll(Func<BO.ProductForList, bool>? func = null);
    public IEnumerable<ProductItem> GetForCatalog();
    public Product GetForManegar(int idProduct);
    public ProductItem GetForClient(int idProduct, Cart c);
    public int Add(Product p);
    public void Delete(int idProduct);
    public void Update(Product p);
    
}
