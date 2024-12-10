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

        try
        {
            while (true)
            {
                grid.SetAtIndex(start, 'X');
                if (grid.Index(start + direction) == '#')
                {
                    direction = direction.Turn90();
                }
                start = start + direction;
            }
        }
        catch (IndexOutOfRangeException) { }

        _testOutputHelper.WriteLine(grid.Sum(x => x.Count(y => y == 'X')));
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

        var count = 0;
        for (var i = 0; i < grid.Length; i++)
            for (var j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] != '.' || start.Equals(new Coordinate(i, j)))
                    continue;
                grid[i][j] = '#';
                if (FindLoop(grid, start, direction))
                    count++;
                grid[i][j] = '.';
            }
        _testOutputHelper.WriteLine(count);
    }

    private bool FindLoop(char[][] grid, Coordinate start, Coordinate direction)
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