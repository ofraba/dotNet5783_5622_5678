using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public Category Category { get; set; }
    public bool InStock { get; set; }
    public override string ToString() => $@"
Product ID = {ID}: 
name: {Name},
amount: {Amount},
category: {Category},
Price: {Price},
in stock?: {InStock}
";
}
