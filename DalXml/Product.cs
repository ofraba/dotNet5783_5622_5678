using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal
{
    internal class Product : IProduct
    {
        public int Add(DO.Product item)
        {
            XElement? Products = XDocument.Load("../Product.xml").Root;
            var list = Products?.Elements().ToList().Where(product => product?.Element("Id")?.Value == item.ID.ToString());
            if (list?.Count() > 0)
                throw new ex2();
            XElement? product = new XElement("Product");
            XElement? Id = new XElement("ID", item.ID);
            product.Add(Id);
            XElement? Name = new XElement("Name", item.Name);
            product.Add(Name);
            XElement? Price = new XElement("Price", item.Price);
            product.Add(Price);
            XElement? Color = new XElement("Color", item.Color);
            product.Add(Color);
            XElement? Category = new XElement("Category", item.Category);
            product.Add(Category);
            XElement? Amount = new XElement("Amount", item.Amount);
            product.Add(Amount);
            Products?.Add(product);
            Products?.Save("../Product.xml");
            return item.ID;
        }
        public DO.Product Get(int id)
        {
            XElement? Products = XDocument.Load("../Product.xml").Root;
            var findThisProduct = Products?.Elements().ToList().Find(product => Convert.ToInt32(product?.Element("Id")?.Value) == id);
            if (findThisProduct == null)
                throw new ex1();
            DO.Category.TryParse(findThisProduct?.Element("Category")?.Value, out DO.Category productCategory);
            return new DO.Product
            {
                ID = Convert.ToInt32(findThisProduct?.Element("ID")?.Value),
                Name = findThisProduct?.Element("Name")?.Value.ToString(),
                Price = Convert.ToInt32(findThisProduct?.Element("Price")?.Value),
                Color = findThisProduct?.Element("Color")?.Value.ToString(),
                Category = productCategory,
                Amount = Convert.ToInt32(findThisProduct?.Element("Amount")?.Value)
            };
        }

        public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
        {
            XElement? Products = XDocument.Load("../Product.xml").Root;
            List<DO.Product> productsList = new List<DO.Product> { };
            Products?.Elements().ToList().ForEach(item =>
            {
                DO.Category.TryParse(item?.Element("Category")?.Value, out DO.Category productCategory);
                productsList.Add(new DO.Product
                {
                    ID = Convert.ToInt32(item?.Element("ID")?.Value),
                    Name = item?.Element("Name")?.Value.ToString(),
                    Price = Convert.ToInt32(item?.Element("Price")?.Value),
                    Color = item?.Element("Color")?.Value.ToString(),
                    Category = productCategory,
                    Amount = Convert.ToInt32(item?.Element("Amount")?.Value),
                });
            });
             return func == null ? productsList : productsList.Where(func);
        }

        public void Delete(int id)
        {
            XElement? Products = XDocument.Load("../Product.xml").Root;
            Products?.Elements().ToList().Find(product => Convert.ToInt32(product?.Element("ID")?.Value) == id)?.Remove();
        }
        public void Update(DO.Product updateProduct)
        {
            Delete(updateProduct.ID);
            Add(updateProduct);
        }

    }
}
