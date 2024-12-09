using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day02(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static int[][] LoadData(){
        string[] lines = InputHelper.ReadInput("02");
        return lines.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var levels = LoadData();
        _testOutputHelper.WriteLine(levels.Count(IsSafe));
    }
        
    [Fact]
    public void Part2()
    {
        var levels = LoadData();
        _testOutputHelper.WriteLine(levels.Count(IsSafeWithDampener));
    }

    private static bool IsSafeWithDampener(int[] level)
        => IsSafe(level) || Enumerable.Range(0, level.Length).Where(i => IsSafe(level.Where((_, j) => j != i))).Any();

    private static bool IsSafe(IEnumerable<int> level)
        => IsAscendingSafe(level) || IsAscendingSafe(level.Reverse());

    private static bool IsAscendingSafe(IEnumerable<int> level)
        => level.Skip(1).Aggregate((true, level.First()), (acc, x) => (acc.Item1 && IsInCorrectRange(acc.Item2, x), x)).Item1;

    private static bool IsInCorrectRange(int x, int y)
        => y > x && x + 4 > y;
}