using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day07(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static (long Result, long[] Values)[] LoadData()
    {
        var lines = InputHelper.ReadInput("07");
        return lines.Select(ParseLine).ToArray();

        static (long, long[]) ParseLine(string line)
        {
            var firstSplit = line.Split(':');
            var secondSplit = firstSplit[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return (long.Parse(firstSplit[0]), secondSplit.Select(long.Parse).ToArray());
        }
    }

    [Fact]
    public void Part1()
    {
        var data = LoadData();
        var totalSum = data.Where(x => CanBeTrue(x.Result, x.Values)).Sum(x => x.Result);
        _testOutputHelper.WriteLine(totalSum);
    }

    private static bool CanBeTrue(long result, long[] values)
    {
        if (values.Length == 1)
            return result == values[0];
        if (values[0] > result)
            return false;

        return CanBeTrue(result, [values[0] * values[1], .. values.Skip(2)])
            || CanBeTrue(result, [values[0] + values[1], .. values.Skip(2)]);
    }

    private static bool CanBeTrueWithConcat(long result, long[] values)
    {
        if (values.Length == 1)
            return result == values[0];
        if (values[0] > result)
            return false;

        return CanBeTrueWithConcat(result, [values[0] * values[1], .. values.Skip(2)])
            || CanBeTrueWithConcat(result, [values[0] + values[1], .. values.Skip(2)])
            || CanBeTrueWithConcat(result, [Concat(values[0], values[1]), .. values.Skip(2)]);

        static long Concat(long a, long b)
            => long.Parse(a.ToString() + b.ToString());
    }

    [Fact]
    public void Part2()
    {
        var data = LoadData();
        var totalSum = data.Where(x => CanBeTrueWithConcat(x.Result, x.Values)).Sum(x => x.Result);
        _testOutputHelper.WriteLine(totalSum);
    }
}