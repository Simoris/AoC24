namespace AoC24.Utils;

public readonly record struct Coordinate(int X, int Y)
{
    public Coordinate((int X, int Y) tuple) : this(tuple.X, tuple.Y) { }

    public static Coordinate operator +(Coordinate a, Coordinate b)
        => new(a.X + b.X, a.Y + b.Y);
    public static Coordinate operator -(Coordinate a, Coordinate b)
        => new(a.X - b.X, a.Y - b.Y);
    public static Coordinate operator *(Coordinate a, int factor)
        => new(factor*a.X, factor*a.Y);
    public static Coordinate operator %(Coordinate a, Coordinate b)
        => new((a.X + b.X) % b.X, (a.Y + b.Y) % b.Y);

    public Coordinate Flip()
        => new(-X, -Y);

    public Coordinate Turn90()
        => new(Y, -X);

    public Coordinate Turn270()
        => new(-Y, X);

    public bool Borders(Coordinate other)
        => Math.Abs(other.X - X) + Math.Abs(other.Y - Y) == 1;
}

public readonly record struct LongCoordinate(long X, long Y)
{
    public LongCoordinate((long X, long Y) tuple) : this(tuple.X, tuple.Y) { }

    public static LongCoordinate operator +(LongCoordinate a, LongCoordinate b)
        => new(a.X + b.X, a.Y + b.Y);
    public static LongCoordinate operator -(LongCoordinate a, LongCoordinate b)
        => new(a.X - b.X, a.Y - b.Y);
    public static LongCoordinate operator *(LongCoordinate a, long factor)
        => new(factor*a.X, factor*a.Y);
    public static LongCoordinate operator *(long factor, LongCoordinate a)
        => new(factor*a.X, factor*a.Y);

    public LongCoordinate Flip()
        => new(-X, -Y);

    public LongCoordinate Turn90()
        => new(Y, -X);

    public LongCoordinate Turn270()
        => new(-Y, X);

    public bool Borders(LongCoordinate other)
        => Math.Abs(other.X - X) + Math.Abs(other.Y - Y) == 1;
}

public static class ArrayExtensions
{
    public static T Index<T>(this T[][] arr, Coordinate index)
        => arr[index.X][index.Y];
    public static void SetAtIndex<T>(this T[][] arr, Coordinate index, T value)
        => arr[index.X][index.Y] = value;

    public static IEnumerable<Coordinate> FindAll<T>(this T[][] arr, T value)
        where T : IEquatable<T>
        => arr.Index()
            .SelectMany(x => x.Item
                .Index()
                .Where(y => y.Item.Equals(value))
                .Select(y => new Coordinate(x.Index, y.Index)))
            .ToArray();
}