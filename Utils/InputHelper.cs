namespace AoC24.Utils;

public static class InputHelper
{
    public static string[] ReadInput(string day)
        => File.ReadAllLines(@$"input\day{day}.txt").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

    public static (string[], string[]) ReadTwoPartInput(string day)
    {
        var lines = File.ReadAllLines(@$"input\day{day}.txt");
        var firstEmpty = lines.Index().First(x => string.IsNullOrWhiteSpace(x.Item)).Index;
        return (lines.Take(firstEmpty).ToArray(), lines.Skip(firstEmpty+1).ToArray());
    }

    public static string[][] ReadBlockInput(string day, int blockSize)
    {
        var lines = File.ReadAllLines(@$"input\day{day}.txt");
        return lines.Index().GroupBy(x => x.Index/(blockSize + 1)).Select(x => x.Select(y => y.Item).Where(y => !string.IsNullOrWhiteSpace(y)).ToArray()).ToArray();
    }
}