using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;


public class ExceptionFromDal : Exception
{
    public ExceptionFromDal(Exception e) : base("exception from dal", e) { }
}

public class exception1 : Exception
{
    public override string Message => "one or more data are invalid";

}

public class exception2 : Exception
{
    public override string Message => "The product exists in one or more of the orders";

}

public class exception3 : Exception
{
    public override string Message => "negative ID number";

}

public class exception4 : Exception
{
    public override string Message => "The order has been sent";

}

public class exception5 : Exception
{
    public override string Message => "The order has already been delivered";

}

public class exception6 : Exception
{
    public override string Message => "Not enough quantity in stock";

}

