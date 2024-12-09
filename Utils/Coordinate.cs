namespace AoC24.Utils;

public readonly record struct Coordinate(int X, int Y)
{
    public static Coordinate operator + (Coordinate a, Coordinate b)
        => new(a.X + b.X, a.Y + b.Y);

    public Coordinate Flip()
        => new(-X, -Y);

    public Coordinate Turn90()
        => new(Y, -X);

    public  Coordinate Turn270()
        => new(-Y, X);
}

public static class ArrayExtensions
{
    public static T Index<T>(this T[][] arr, Coordinate index)
        => arr[index.X][index.Y];
}