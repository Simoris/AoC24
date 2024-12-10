namespace AoC24.Utils;

public static class EnumerableExtensions
{
    public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
    {
        foreach (var item in items)
            hashSet.Add(item);
    }
}
