using System.Text.RegularExpressions;
using System.Linq;
using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day05(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static ((int, int)[], int[][]) LoadData()
    {
        var (first, second) = InputHelper.ReadTwoPartInput("05");
        return (first.Select(Split).ToArray(), second.Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray());

        static (int, int) Split(string s)
        {
            var splitted = s.Split('|');
            return (int.Parse(splitted[0]),int.Parse(splitted[1]));
        }
    }

    [Fact]
    public void Part1()
    {
        var (rules, pages) = LoadData();
        var result = pages.Where(x => IsValid(x, rules)).Select(Middle).Sum();
        _testOutputHelper.WriteLine(result);
    }

    [Fact]
    public void Part2()
    {
        var (rules, pages) = LoadData();
        var result = pages.Where(x => !IsValid(x, rules)).Select(x => Order(x, rules)).Select(Middle).Sum();
        _testOutputHelper.WriteLine(result);
    }

    private static bool IsValid(int[] page, (int, int)[] rules)
        => rules.All(rule => IsValid(page, rule));

    private static bool IsValid(int[] page, (int, int) rule)
        => !page.Contains(rule.Item1) || !page.Contains(rule.Item2)
        || page.Skip(page.Index().First(x => x.Item == rule.Item1).Index).Contains(rule.Item2);

    private static int[] Order(int[] page, (int, int)[] rules)
    {
        foreach (var rule in rules.Concat(rules).Concat(rules).Concat(rules).Concat(rules))
        {
            if (!IsValid(page, rule))
            {
                var pos1 = page.Index().Single(x => x.Item == rule.Item1).Index;
                var pos2 = page.Index().Single(x => x.Item == rule.Item2).Index;
                page[pos1] = rule.Item2;
                page[pos2] = rule.Item1;
            }
        }
        return page;
    }

    private static int Middle(int[] page)
        => page[(page.Length-1)/2];

}