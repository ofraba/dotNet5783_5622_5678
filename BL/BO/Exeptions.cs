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

public class dataIsntInvalid : Exception
{
    public override string Message => "one or more data are invalid";

}

public class productExsistInOrder : Exception
{
    public override string Message => "The product exists in one or more orders";

}

public class negativeIDnumber : Exception
{
    public override string Message => "negative ID number";

}

public class TheOrderHasBeenSent : Exception
{
    public override string Message => "The order has been sent";

}

public class orderHasBeenDelivered : Exception
{
    public override string Message => "The order has already been delivered";

}

public class notEnoughAmount : Exception
{
    public override string Message => "Not enough quantity in stock";

}

public class nullException : Exception
{
    public override string Message => "it's null";

}

