using System;

public class InvalidQuantityException : Exception
{
    public InvalidQuantityException(string message) : base(message) { }
}
