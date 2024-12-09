using System.Text.RegularExpressions;
using System.Linq;
using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day04(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static char[][] LoadData() => InputHelper.ReadInput("04").Select(x => x.ToCharArray()).ToArray();

    private readonly Coordinate[] _directions = [new(1,1), new(1,-1), new(1,0), new(-1,1), new(-1,-1), new(-1,0), new(0,1), new(0,-1)];

    private static IEnumerable<Coordinate> AllPoints(int height, int width)
        => Enumerable.Range(0, height).SelectMany(x => Enumerable.Range(0, width).Select(y=> new Coordinate(x,y)));

    [Fact]
    public void Part1()
    {
        var grid = LoadData();
        var count = AllPoints(grid.Length, grid[0].Length).SelectMany(start => _directions.Where(dir => SearchInDirection(grid, start, dir, "XMAS"))).Count();
        _testOutputHelper.WriteLine(count);
    }
    
    [Fact]
    public void Part2()
    {
        var grid = LoadData();
        var count = AllPoints(grid.Length, grid[0].Length).SelectMany(start => _directions.Where(d => d.X*d.Y != 0).Where(dir => SearchXMas(grid, start, dir))).Count();
        _testOutputHelper.WriteLine(count);
    }

    private static bool SearchInDirection(char[][] grid, Coordinate start, Coordinate direction, IEnumerable<char> str)
    {
        if (!str.Any())
            return true;
        try
        {
            if (grid.Index(start) != str.First())
                return false;
            
            return SearchInDirection(grid, start + direction, direction, str.Skip(1));
        }
        catch
        {
            return false;
        }
    }

    private static bool SearchXMas(char[][] grid, Coordinate start, Coordinate baseDirection)
    {
        if (grid.Index(start) != 'A')
            return false;

        try
        {
            if (grid.Index(start+baseDirection) != 'S')
                return false;
            if (grid.Index(start+baseDirection.Turn90()) != 'S')
                return false;
            if (grid.Index(start+baseDirection.Turn270()) != 'M')
                return false;
            if (grid.Index(start+baseDirection.Flip()) != 'M')
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }
}