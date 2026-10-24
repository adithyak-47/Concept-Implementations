using System.Collections;

namespace HashMap;
internal class LinkedList<T> : IEnumerable<T>
{
    public Node<T>? Head { get; set; } = null;
    public int Count { get; set; } = 0;
    public bool IsEmpty => Count == 0;
    public void Add(T value)
    {
        var node = new Node<T>(value);
        if (Head == null)
        {
            Head = node;
        }
        else
        {
            var current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Value = value;
            Count++;
        }
    }

    public void Remove(Predicate<T> predicate)
    {
        if (Head == null)
        {
            return;
        }
        else if (predicate(Head.Value))
        {
            Head = null;
            Count--;
            return;
        }
        else
        {
            var current = Head.Next;
            while (current != null)
            {
                if (predicate(current.Value))
                {
                    current.Next = current.Next;
                    Count--;
                    return;
                }
            }
        }
    }

    public T Find(Predicate<T> predicate)
    {
        var current = Head;
        while (current != null)
        {
            if (predicate(current.Value))
            {
                return current.Value;
            }
            current = current.Next;
        }

        throw new Exception("value not found");
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = Head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class Node<T>(T value)
{
    public T Value { get; set; } = value;
    public Node<T>? Next { get; set; } = null;
}
