using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T>
{
    private readonly List<T> items = new();

    public void Add(T item) => items.Add(item);

    public List<T> GetAll() => new List<T>(items);

    public T? GetById(Func<T, bool> predicate) => items.FirstOrDefault(predicate);

    public bool Remove(Func<T, bool> predicate)
    {
        var item = items.FirstOrDefault(predicate);
        if (item == null) return false;
        items.Remove(item);
        return true;
    }
}
