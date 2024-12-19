using AoC24.Utils;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public partial class Day14(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    [GeneratedRegex(@"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)")]
    public partial Regex LineParser { get; }

    private (Coordinate Position, Coordinate Velocity)[] LoadData()
    {
        var lines = InputHelper.ReadInput("14");
        return lines
            .Select(x => LineParser.Match(x).Groups)
            .Select(x => (new Coordinate(int.Parse(x[1].Value), int.Parse(x[2].Value)), new Coordinate(int.Parse(x[3].Value), int.Parse(x[4].Value))))
            .ToArray();
    }

    [Fact]
    public void Part1()
    {
        var robots = LoadData();
        var room = new Coordinate(11, 7);

    }

    private Coordinate Move(Coordinate start, Coordinate velocity, Coordinate room, int times)
        => (start + velocity) % room;
    private Coordinate Move(Coordinate start, Coordinate velocity, Coordinate room)
        => (start + velocity) % room;
}