using System;

public class DuplicateItemException : Exception
{
    public DuplicateItemException(string message) : base(message) { }
}
