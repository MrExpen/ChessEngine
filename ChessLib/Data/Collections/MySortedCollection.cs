using System.Collections;

namespace ChessLib.Data.Collections;

public class MySortedCollection<T> : ICollection<T>
{
    private readonly List<T> _collection;
    private readonly IComparer<T>? _comparer;

    public MySortedCollection(IComparer<T>? comparer = null)
    {
        _comparer = comparer;
        _collection = new();
    }

    public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T item) => _collection.Insert(_collection.BinarySearch(item, _comparer) * -1 - 1, item);

    public void Clear() => _collection.Clear();

    public bool Contains(T item) => _collection.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);

    public bool Remove(T item) => _collection.Remove(item);

    public T PopLast()
    {
        var last = _collection[^1];
        _collection.RemoveAt(_collection.Count - 1);
        return last;
    }

    public T PeekLast() => _collection[^1];

    public T PopFirst()
    {
        var first = _collection[0];
        _collection.RemoveAt(0);
        return first;
    }

    public T PeekFirst() => _collection[0];

    public int Count => _collection.Count;
    public bool IsReadOnly => (_collection as ICollection<T>).IsReadOnly;
}