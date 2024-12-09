using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day01(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static (int[] Left, int[] Right) LoadData(){
        string[] lines = InputHelper.ReadInput("01");

        var left = lines.Select(x => int.Parse(x.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0])).ToArray();
        var right =lines.Select(x => int.Parse(x.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1])).ToArray();
        return (left, right);
    }

    [Fact]
    public void Part1()
    {
        var (left, right) = LoadData();
        var distance = left.Order().Zip(right.Order()).Select(x => Math.Abs(x.First - x.Second)).Sum();
        _testOutputHelper.WriteLine(distance);
    }

    [Fact]
    public void Part2()
    {
        var (left, right) = LoadData();
        var similarity = left.Select(x => x*right.Count(y => y == x)).Sum();
        _testOutputHelper.WriteLine(similarity);
    }
}