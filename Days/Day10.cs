using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day10(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static int[][] LoadData()
    {
        var lines = InputHelper.ReadInput("10");
        return lines.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var map = LoadData();
        var trailheads = map.FindAll(0);
        var sum = trailheads.Select(x => GetScore(map, x)).Sum();
        _testOutputHelper.WriteLine(sum);
    }

    [Fact]
    public void Part2()
    {
        var map = LoadData();
        var trailheads = map.FindAll(0);
        var sum = trailheads.Select(x => GetRating(map, x)).Sum();
        _testOutputHelper.WriteLine(sum);
    }

    private static int GetScore(int[][] map, Coordinate trailhead)
    {
        return FindTops(map, trailhead).Distinct().Count();
    }

    private static int GetRating(int[][] map, Coordinate trailhead)
    {
        return FindTops(map, trailhead).Count();
    }

    private static readonly Coordinate[] _directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];
    private static List<Coordinate> FindTops(int[][] map, Coordinate start)
    {
        var startValue = map.Index(start);
        if (startValue == 9)
            return [start];

        List<Coordinate> result = [];
        foreach (var direction in _directions)
        {
            try
            {
                if (map.Index(start + direction) == startValue + 1)
                    result.AddRange(FindTops(map, start + direction));
            }
            catch (IndexOutOfRangeException) { }
        }
        return result;
    }
}