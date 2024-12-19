using System.Text.RegularExpressions;
using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public partial class Day13(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    [GeneratedRegex(@"Button [AB]: X\+(\d+), Y\+(\d+)")]
    private partial Regex ButtonRegex {get;}
    private LongCoordinate ParseButton(string button)
    {
        var match =  ButtonRegex.Match(button);
        return new(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
    }

    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private partial Regex PrizeRegex {get;}
    private LongCoordinate ParsePrize(string prize)
    {
        var match =  PrizeRegex.Match(prize);
        return new(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
    }

    private (LongCoordinate A, LongCoordinate B, LongCoordinate Prize)[] LoadData()
    {
        var blocks = InputHelper.ReadBlockInput("13",3);
        return blocks.Select(x => (ParseButton(x[0]), ParseButton(x[1]), ParsePrize(x[2]))).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var data = LoadData();
        var totalCost = data.Select(x => Solve(x.A, x.B, x.Prize)).Sum();
        _testOutputHelper.WriteLine(totalCost);
    }

    [Fact]
    public void Part2()
    {
        const long offset = 10000000000000;
        var data = LoadData().Select(x => (x.A, x.B, Prize: x.Prize with {X = x.Prize.X + offset, Y = x.Prize.Y + offset}));
        var totalCost = data.Select(x => Solve(x.A, x.B, x.Prize)).Sum();
        _testOutputHelper.WriteLine(totalCost);
    }
    
    private static long Solve(LongCoordinate A, LongCoordinate B, LongCoordinate Prize)
    {
        var bFactor = -(double)A.X/A.Y;
        var b = (long) Math.Round((Prize.X + bFactor*Prize.Y)/(B.X + bFactor*B.Y));

        var aFactor = -(double)B.X/B.Y;
        var a = (long) Math.Round((Prize.X + aFactor*Prize.Y)/(A.X + aFactor*A.Y));

        if (a*A+b*B!= Prize)
            return 0;

        return 3*a+b;
    }
}