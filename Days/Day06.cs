using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day06(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static char[][] LoadData()
    {
        var lines = InputHelper.ReadInput("06");
        return lines.Select(x => x.ToCharArray()).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var grid = LoadData();
        var row = grid.Index().Single(x => x.Item.Contains('^'));
        var col = row.Item.Index().Single(x => x.Item == '^').Index;

        var start = new Coordinate(row.Index, col);
        var direction = new Coordinate(-1, 0);

        grid = WalkOut(grid, start, direction);

        _testOutputHelper.WriteLine(grid.Sum(x => x.Count(y => y == 'X')));
    }

    private static char[][] WalkOut(char[][] grid, Coordinate start, Coordinate direction)
    {
        try
        {
            while (true)
            {
                grid.SetAtIndex(start, 'X');
                if (grid.Index(start + direction) == '#')
                {
                    direction = direction.Turn90();
                }
                start += direction;
            }
        }
        catch (IndexOutOfRangeException) { }
        return grid;
    }

    private void PrintGrid(char[][] grid, Action<string> print = null)
    {
        print ??= _testOutputHelper.WriteLine;
        foreach (var line in grid)
            print(new string(line));
    }

    [Fact]
    public void Part2()
    {
        var grid = LoadData();
        var row = grid.Index().Single(x => x.Item.Contains('^'));
        var col = row.Item.Index().Single(x => x.Item == '^').Index;

        var start = new Coordinate(row.Index, col);
        grid.SetAtIndex(start, '.');
        var direction = new Coordinate(-1, 0);

        var walkWay = WalkOut(grid.Select(x => x.ToArray()).ToArray(), start, direction);
        var positions = walkWay.FindAll('X');

        var count = 0;
        foreach (var position in positions.Where(x => grid.Index(x) == '.' && start != x))
        {
            grid.SetAtIndex(position, '#');
            if (FindLoop(grid, start, direction))
                count++;
            grid.SetAtIndex(position, '.');
        }

        _testOutputHelper.WriteLine(count);
    }

    private static bool FindLoop(char[][] grid, Coordinate start, Coordinate direction)
    {
        try
        {
            for (int i = 0; i < grid.Length * grid[0].Length; i++)
            {
                if (grid.Index(start + direction) == '#')
                    direction = direction.Turn90();
                else
                    start += direction;
            }
            return true;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }
}