using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day08(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static char[][] LoadData()
    {
        var lines = InputHelper.ReadInput("08");
        return lines.Select(x => x.ToCharArray()).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var grid = LoadData();
        var values = grid.SelectMany(x => x).Where(x => x != '.').ToHashSet();

        var antiNodes = new HashSet<Coordinate>();
        foreach (var value in values)
        {
            var positions = grid.FindAll(value).ToArray();
            antiNodes.AddRange(GenerateAntinodes(positions));
        }

        var count = antiNodes
            .Where(WithinGrid(grid))
            .Count();
        _testOutputHelper.WriteLine(count);
    }

    [Fact]
    public void Part2()
    {
        var grid = LoadData();
        var values = grid.SelectMany(x => x).Where(x => x != '.').ToHashSet();

        var antiNodes = new HashSet<Coordinate>();
        foreach (var value in values)
        {
            var positions = grid.FindAll(value).ToArray();
            antiNodes.AddRange(GenerateResonantAntinodes(positions, WithinGrid(grid)));
        }

        var count = antiNodes
            .Count();
        _testOutputHelper.WriteLine(count);
    }

    private static Func<Coordinate, bool> WithinGrid(char[][] grid)
    {
        return x => x.X < grid.Length && x.X >= 0 && x.Y < grid[0].Length && x.Y >= 0;
    }

    private static HashSet<Coordinate> GenerateAntinodes(Coordinate[] nodes)
    {
        var antinodes = new HashSet<Coordinate>();
        for (var i = 1; i < nodes.Length; i++)
            for (var j = 0; j < i; j++)
                antinodes.AddRange(GenerateAntinodes(nodes[i], nodes[j]));
        return antinodes;
    }

    private static IEnumerable<Coordinate> GenerateAntinodes(Coordinate node1, Coordinate node2)
    {
        var dif = node1 - node2;
        return [node1 + dif, node2 - dif];
    }

    private static HashSet<Coordinate> GenerateResonantAntinodes(Coordinate[] nodes, Func<Coordinate, bool> inGrid)
    {
        var antinodes = new HashSet<Coordinate>();
        for (var i = 1; i < nodes.Length; i++)
            for (var j = 0; j < i; j++)
                antinodes.AddRange(GenerateResonantAntinodes(nodes[i], nodes[j], inGrid));
        return antinodes;
    }

    private static IEnumerable<Coordinate> GenerateResonantAntinodes(Coordinate node1, Coordinate node2, Func<Coordinate, bool> inGrid)
    {
        var dif = node1 - node2;

        while (inGrid(node1))
        {
            yield return node1;
            node1 += dif;
        }

        while (inGrid(node2))
        {
            yield return node2;
            node2 -= dif;
        }
    }
}