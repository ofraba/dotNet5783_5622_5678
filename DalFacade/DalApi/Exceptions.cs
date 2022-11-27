using System;

public class ex1: Exception 
{
    public override string Message => "The object is exsist";
}

public class ex2 : Exception
{
    public override string Message => "The object is already exsist";
}