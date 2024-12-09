using System.Text.RegularExpressions;
using System.Linq;
using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public partial class Day03(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static string[] LoadData() => InputHelper.ReadInput("03");

    [Fact]
    public void Part1()
    {
        var lines = LoadData();
        _testOutputHelper.WriteLine(lines.SelectMany(Multiply).Sum());
    }

    [Fact]
    public void Part2()
    {
        var lines = LoadData();
        _testOutputHelper.WriteLine(GetActivatedParts("do()" + string.Join("", lines)).SelectMany(Multiply).Sum());
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private partial Regex FindMultiplies { get; }

    [GeneratedRegex(@"do\(\)(.*?)don't\(\)")]
    private partial Regex FindActive { get; }

    private int[] Multiply(string line)
        => FindMultiplies.Matches(line).Select(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value)).ToArray();

    private string[] GetActivatedParts(string line)
        => FindActive.Matches(line).Select(x => x.Groups[1].Value).ToArray();
}