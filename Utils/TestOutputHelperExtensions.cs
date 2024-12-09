using Xunit.Abstractions;

namespace AoC24.Utils;

public static class TestOutputHelperExtensions
{
    public static void WriteLine(this ITestOutputHelper testOutputHelper, int num)
        => testOutputHelper.WriteLine(num.ToString());
    public static void WriteLine(this ITestOutputHelper testOutputHelper, long num)
        => testOutputHelper.WriteLine(num.ToString());
}