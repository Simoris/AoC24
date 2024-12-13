using AoC24.Utils;
using Xunit;
using Xunit.Abstractions;

namespace AoC24.Days;

public class Day09(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    private static int[] LoadData()
    {
        var lines = InputHelper.ReadInput("09");
        return lines.First().ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
    }

    private record Memory(int Size, int? Id)
    {
        public bool Filled => Id.HasValue;
    }

    [Fact]
    public void Part2()
    {
        var map = LoadData();
        var memory = ToMemory(map);
        memory = Compact(memory.ToList());
        var checksum = CheckSum(memory);
        _testOutputHelper.WriteLine(checksum);
    }

    [Fact]
    public void Part1()
    {
        var map = LoadData();
        var memory = ToMemory(map);
        memory = CompactFragment(memory.ToList());
        var checksum = CheckSum(memory);
        _testOutputHelper.WriteLine(checksum);
    }

    private static Memory[] ToMemory(int[] map)
        => map.Index().Select(x => new Memory(x.Item, x.Index % 2 == 0 ? x.Index / 2 : null)).ToArray();

    private static Memory[] Compact(List<Memory> memory)
    {
        for (var i = memory.Count - 1; i >= 0; i--)
        {
            if (!memory[i].Filled)
                continue;

            //OPTIMIZATION: don't search empties again and again, index first
            var space = memory
                .Index()
                .FirstOrDefault(x => !x.Item.Filled && x.Item.Size >= memory[i].Size, (Index: -1, Item: new Memory(0, 0)));
            if (space.Index == -1)
                continue;
            if (space.Index > i)
                continue;
            if (space.Item.Size == memory[i].Size)
            {
                memory[space.Index] = memory[i];
                memory[i] = memory[i] with { Id = null };
            }
            else
            {
                memory[space.Index] = memory[i];
                memory[i] = memory[i] with { Id = null };
                memory.Insert(space.Index + 1, new Memory(space.Item.Size - memory[i].Size, null));
                i++;
            }
        }
        return memory.ToArray();
    }

    private static Memory[] CompactFragment(List<Memory> memory)
    {
        var freeIndex = 0;
        for (var i = memory.Count - 1; i >= 0; i--)
        {
            if (!memory[i].Filled)
                continue;
            while (memory[freeIndex].Filled) freeIndex++;

            if (freeIndex > i)
                break;
            if (memory[freeIndex].Size == memory[i].Size)
            {
                memory[freeIndex] = memory[i];
                memory[i] = memory[i] with { Id = null };
            }
            else if (memory[freeIndex].Size > memory[i].Size)
            {
                var oldSize = memory[freeIndex].Size;
                memory[freeIndex] = memory[i];
                memory[i] = memory[i] with { Id = null };
                memory.Insert(freeIndex + 1, new Memory(oldSize - memory[i].Size, null));
                i++;
            }
            else
            {
                memory[freeIndex] = memory[freeIndex] with { Id = memory[i].Id };
                memory[i] = memory[i] with { Size = memory[i].Size - memory[freeIndex].Size };
                memory.Insert(i + 1, new Memory(memory[freeIndex].Size, null));
                i++;
            }
        }
        return memory.ToArray();
    }

    private static long CheckSum(Memory[] memory)
    {
        List<int> expandedMemory = [];
        foreach (var mem in memory)
            expandedMemory.AddRange(Enumerable.Range(0, mem.Size).Select(x => mem.Id ?? -1));
        return CheckSum(expandedMemory.ToArray());
    }
    private static long CheckSum(int[] map)
        => map.Index().Select(x => (long)(x.Item != -1 ? x.Item * x.Index : 0)).Sum();
}