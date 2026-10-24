namespace HashMap;

/// <summary>
/// Custom implementation of a hash map data structure that uses separate chaining for collision resolution. It allows adding, removing, and retrieving key-value pairs.
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
/// TODO: Implement a method to check if a key exists in the hash map. 
/// TODO: Implement a method to get all keys in the hash map.
/// TODO: Implement a method to get all values in the hash map.
/// TODO: Implement a method to clear the hash map.
/// TODO: Implement a method to get the current size of the hash map.
/// TODO: Implement a method similar to TryGetValue in C#'s Dictionary class that returns a boolean indicating whether the key exists and outputs the value if it does.
internal class HashSharp<K, V>
{
    private readonly int initialCapacity = 16;
    private readonly int loadFactor = 75;
    private int currentSize = 0;

    private LinkedList<Entry<K, V>>[] buckets;

    public HashSharp()
    {
        buckets = new LinkedList<Entry<K, V>>[initialCapacity];
    }

    public void Add(K key, V value)
    {
        if (currentSize >= initialCapacity * (loadFactor / 100))
        {
            Resize();
        }

        var index = Hash(key.GetHashCode(), buckets.Length);
        if (buckets[index] == null)
        {
            buckets[index] = new LinkedList<Entry<K, V>>();
        }
        buckets[index].Add(new Entry<K, V>(key, value));
        currentSize++;
    }

    public void Remove(K key)
    {
        var index = Hash(key.GetHashCode(), buckets.Length);
        if (buckets[index] != null)
        {
            buckets[index].Remove(entry => entry.Key.Equals(key));
            currentSize--;
        }
    }

    public V Get(K key)
    {
        var index = Hash(key.GetHashCode(), buckets.Length);
        if (buckets[index] != null)
        {
            return buckets[index].Find(e => e.Key.Equals(key)).Value;
        }
        throw new KeyNotFoundException();
    }
    

    private static int Hash(int hash, int capacity)
    {
        return (int)((uint)hash & (uint)capacity);
    }

    private void Resize()
    {
        var newCapacity = initialCapacity * 2;
        var newBuckets = new LinkedList<Entry<K, V>>[newCapacity];

        foreach (var bucket in buckets)
        {
            if (bucket != null)
            {
                foreach (var entry in bucket)
                {
                    var index = Hash(entry.Key.GetHashCode(), newCapacity);
                    if (newBuckets[index] == null)
                    {
                        newBuckets[index] = new LinkedList<Entry<K, V>>();
                    }
                    newBuckets[index].Add(entry);
                }
            }
        }
        buckets = newBuckets;
    }

}
