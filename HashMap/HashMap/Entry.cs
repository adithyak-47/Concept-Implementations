namespace HashMap;
internal class Entry<TKey, TValue>(TKey key, TValue value)
{
    public TKey Key { get; set; } = key;
    public TValue Value { get; set; } = value;
}
