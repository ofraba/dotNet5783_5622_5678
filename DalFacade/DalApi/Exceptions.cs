using System;

public class ex1: Exception 
{
    public override string Message => "The object is not exsist";
}

public class ex2 : Exception
{
    public override string Message => "The object is already exsist";
}


