using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day12(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static char[][] LoadData()
    {
        var lines = InputHelper.ReadInput("12");
        return lines.Select(x => x.ToCharArray()).ToArray();
    }

    private record Region(Coordinate[] Plots)
    {
        public int Area => Plots.Length;
        public int FenceCost() => Area * CalculatePerimeter();
        public int DiscountedCost() => Area * CalculateSides();

        private int CalculatePerimeter()
            => GetUniqueSides().Count();

        private record Side(Coordinate A, Coordinate B)
        {
            public Coordinate Direction => A - B;
            public int StableCoordinate => A.X == B.X ? A.X : A.Y;
            public int UnstableBase => A.X == B.X ? A.Y : A.X;
            public Side Normalize()
                => A.X >= B.X && A.Y >= B.Y ? new(B, A) : this;
        }

        private IEnumerable<Side> GetUniqueSides()
            => Plots.SelectMany<Coordinate, Side>(x => [
                    new Side(x, x with {X = x.X+1}),
                    new Side(x with {X = x.X+1},new(x.X + 1, x.Y +1)),
                    new Side(new(x.X + 1, x.Y +1), x with {Y = x.Y+1}),
                    new Side(x with {Y = x.Y+1}, x),])
                .GroupBy(x => x.Normalize())
                .Where(x => x.Count() == 1)
                .SelectMany(x => x);

        private int CalculateSides()
        {
            return GetUniqueSides()
                .GroupBy(x => (x.Direction, x.StableCoordinate))
                .Select(x => CountContinousRuns(x.Select(y => y.UnstableBase).Order()))
                .Sum();

            static int CountContinousRuns(IEnumerable<int> orderedNums)
                => orderedNums
                    .Aggregate((Count: 1, Last: orderedNums.First() - 1),
                        (acc, num) => (acc.Last + 1 == num ? acc.Count : acc.Count + 1, num)).Count;
        }
    }

    [Fact]
    public void Part1()
    {
        var grid = LoadData();
        var regions = FindRegions(grid);
        var totalCost = regions.Sum(x => x.FenceCost());
        _testOutputHelper.WriteLine(totalCost);
    }

    [Fact]
    public void Part2()
    {
        var grid = LoadData();
        var regions = FindRegions(grid);
        var totalCost = regions.Sum(x => x.DiscountedCost());
        _testOutputHelper.WriteLine(totalCost);
    }

    private static IEnumerable<Region> FindRegions(char[][] grid)
    {
        var plotsByCrop = grid
            .Index()
            .SelectMany(line => line.Item.Index().Select(plot => (Y: line.Index, X: plot.Index, Crop: plot.Item)))
            .ToLookup(x => x.Crop);

        foreach (var largerRegion in plotsByCrop)
        {
            var set = largerRegion.Select<(int X, int Y, char), Coordinate?>(x => new Coordinate(x.X, x.Y)).ToHashSet();
            while (set.Count != 0)
            {
                List<Coordinate> plots = [];
                Coordinate? toAdd = set.First();
                while (toAdd != null)
                {
                    set.Remove(toAdd.Value);
                    plots.Add(toAdd.Value);
                    toAdd = set.FirstOrDefault(x => x.HasValue && plots.Any(y => x.Value.Borders(y)));
                }
                yield return new(plots.ToArray());
            }
        }
    }
}