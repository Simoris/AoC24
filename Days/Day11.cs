using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day11(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static long[] LoadData()
    {
        var lines = InputHelper.ReadInput("11");
        return lines.Single().Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var input = LoadData();
        var count = input.Select(x => CountAfterNBlinks(x, 25)).Sum();
        _testOutputHelper.WriteLine(count);
    }

    [Fact]
    public void Part2()
    {
        var input = LoadData();
        var count = input.Select(x => CountAfterNBlinks(x, 75)).Sum();
        _testOutputHelper.WriteLine(count);
    }

    private static readonly Dictionary<(long, long), long> _cache = [];

    private static long CountAfterNBlinks(long stone, long numBlinks)
    {
        if (_cache.TryGetValue((stone, numBlinks), out var result))
            return result;

        if (numBlinks == 0)
            return 1;

        var newStones = Blink(stone);
        result = newStones.Select(x => CountAfterNBlinks(x, numBlinks - 1)).Sum();
        _cache.Add((stone, numBlinks), result);
        return result;
    }

    private static long[] Blink(long stone)
    {
        if (stone == 0)
            return [1];
        var stoneString = stone.ToString();
        if (stoneString.Length % 2 == 0)
        {
            return [
                long.Parse(stoneString.Substring(0, stoneString.Length / 2)),
                long.Parse(stoneString.Substring(stoneString.Length / 2, stoneString.Length / 2))];
        }
        else
        {
            return [stone * 2024];
        }
    }
}